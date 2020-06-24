using AirMonitor.Data;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AirMonitor.Helpers
{
    public class DatabaseHelper : IDisposable
    {
        private SQLiteAsyncConnection dbAsyncConnection;

        public async Task InitDatabase()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "AirMonitor.db");

            dbAsyncConnection = new SQLiteAsyncConnection(databasePath, 
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);

            await dbAsyncConnection.CreateTableAsync<InstallationEntity>();
            await dbAsyncConnection.CreateTableAsync<MeasurementEntity>();
            await dbAsyncConnection.CreateTableAsync<MeasurementItemEntity>();
            await dbAsyncConnection.CreateTableAsync<MeasurementValue>();
            await dbAsyncConnection.CreateTableAsync<AirQualityIndex>();
            await dbAsyncConnection.CreateTableAsync<AirQualityStandard>();
        }

        public void Dispose()
        {
            dbAsyncConnection?.CloseAsync();
            dbAsyncConnection = null;
        }

        public async Task SaveInstallations(List<Installation> installations)
        {
            var entities = installations.Select(x => new InstallationEntity(x)).ToList();

            await dbAsyncConnection.RunInTransactionAsync((db) =>
            {
                db.DeleteAll<InstallationEntity>();
                db.InsertAll(entities, false);
            });
        }

        public async Task SaveMeasurements(List<Measurement> measurements)
        {
            await dbAsyncConnection.RunInTransactionAsync((db) =>
            {
                db.DeleteAll<MeasurementEntity>();
                db.DeleteAll<MeasurementItemEntity>();
                db.DeleteAll<MeasurementValue>();
                db.DeleteAll<AirQualityIndex>();
                db.DeleteAll<AirQualityStandard>();

                foreach (var measurement in measurements)
                {
                    db.InsertAll(measurement.Current.Values, false);
                    db.InsertAll(measurement.Current.Indexes, false);
                    db.InsertAll(measurement.Current.Standards, false);

                    var measurementItemEntity = new MeasurementItemEntity(measurement.Current);

                    db.Insert(measurementItemEntity);
                    db.Insert(new MeasurementEntity(measurement.Installation.Id, measurementItemEntity.Id));
                }
            });
        }

        public async Task<List<Installation>> GetInstallations()
        {
            var query = dbAsyncConnection.Table<InstallationEntity>();
            var result = await query.ToListAsync();

            var installations = result.Select(x => new Installation(x)).ToList();
            return installations;
        }

        public async Task<List<Measurement>> GetMeasurements()
        {
            var measurementEntitiesQuery = dbAsyncConnection.Table<MeasurementEntity>();
            var measurementEntities = await measurementEntitiesQuery.ToListAsync();

            var measurements = new List<Measurement>(measurementEntities.Count);
            foreach (var measurementEntity in measurementEntities)
            {
                var entityInstallationEntityQuery = dbAsyncConnection.Table<InstallationEntity>()
                    .Where(x => x.Id == measurementEntity.InstallationId);
                var installationEntity = await entityInstallationEntityQuery.FirstAsync();

                var installation = new Installation(installationEntity);

                var measurementItemEntityQuery = dbAsyncConnection.Table<MeasurementItemEntity>()
                    .Where(x => x.Id == measurementEntity.CurrentId);
                var measurementItemEntity = await measurementItemEntityQuery.FirstAsync();

                var valuesIndexes = JsonConvert.DeserializeObject<List<int>>(measurementItemEntity.Values);
                var indexesIndexes = JsonConvert.DeserializeObject<List<int>>(measurementItemEntity.Indexes);
                var standardsIndexes = JsonConvert.DeserializeObject<List<int>>(measurementItemEntity.Standards);

                var valuesQuery = dbAsyncConnection.Table<MeasurementValue>().Where(x => valuesIndexes.Contains(x.Id));
                var values = await valuesQuery.ToListAsync();

                var indexesQuery = dbAsyncConnection.Table<AirQualityIndex>().Where(x => indexesIndexes.Contains(x.Id));
                var indexes = await indexesQuery.ToListAsync();

                var standardsQuery = dbAsyncConnection.Table<AirQualityStandard>().Where(x => standardsIndexes.Contains(x.Id));
                var standards = await standardsQuery.ToListAsync();

                var current = new MeasurementItem
                {
                    FromDateTime = measurementItemEntity.FromDateTime,
                    TillDateTime = measurementItemEntity.TillDateTime,
                    Values = values,
                    Indexes = indexes,
                    Standards = standards
                };

                var measurement = new Measurement
                {
                    Installation = installation,
                    Current = current
                };

                measurements.Add(measurement);
            }

            return measurements;
        }
    }
}
