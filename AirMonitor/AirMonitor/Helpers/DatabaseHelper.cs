using AirMonitor.Data;
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

        public void InitDatabase()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "AirMonitor.db");

            dbAsyncConnection = new SQLiteAsyncConnection(databasePath, 
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);

            dbAsyncConnection.CreateTableAsync<InstallationEntity>();
            dbAsyncConnection.CreateTableAsync<MeasurementEntity>();
            dbAsyncConnection.CreateTableAsync<MeasurementItemEntity>();
            dbAsyncConnection.CreateTableAsync<MeasurementValue>();
            dbAsyncConnection.CreateTableAsync<AirQualityIndex>();
            dbAsyncConnection.CreateTableAsync<AirQualityStandard>();
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
    }
}
