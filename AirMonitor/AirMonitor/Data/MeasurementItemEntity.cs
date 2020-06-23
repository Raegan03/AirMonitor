using Newtonsoft.Json;
using SQLite;
using System;
using System.Linq;

namespace AirMonitor.Data
{
    [Serializable, Table("measurement_item_entity")]
    public class MeasurementItemEntity
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("from_date_time")]
        public DateTime FromDateTime { get; set; }

        [Column("from_date_time")]
        public DateTime TillDateTime { get; set; }

        [Column("from_date_time")]
        public string Values { get; set; }

        [Column("from_date_time")]
        public string Indexes { get; set; }

        [Column("from_date_time")]
        public string Standards { get; set; }

        public MeasurementItemEntity() { }

        public MeasurementItemEntity(MeasurementItem measurementItem)
        {
            FromDateTime = measurementItem.FromDateTime;
            TillDateTime = measurementItem.TillDateTime;

            Values = JsonConvert.SerializeObject(measurementItem.Values.Select(x => x.Id).ToList());
            Indexes = JsonConvert.SerializeObject(measurementItem.Indexes.Select(x => x.Id).ToList());
            Standards = JsonConvert.SerializeObject(measurementItem.Standards.Select(x => x.Id).ToList());
        }
    }
}
