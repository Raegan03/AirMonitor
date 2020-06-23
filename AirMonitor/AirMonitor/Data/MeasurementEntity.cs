using SQLite;
using System;

namespace AirMonitor.Data
{
    [Serializable, Table("measurement_entity")]
    public class MeasurementEntity
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Indexed]
        [Column("current_id")]
        public int CurrentId { get; set; }

        [Column("installation_id")]
        public string InstallationId { get; set; }

        //[Column("history_ids")]
        //public string HistoryIds { get; set; }

        //[Column("forecast_ids")]
        //public string ForecastIds { get; set; }

        public MeasurementEntity()
        {

        }

        public MeasurementEntity(string installationId, int currentId)
        {
            CurrentId = currentId;
            InstallationId = installationId;
        }
    }
}
