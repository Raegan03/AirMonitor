using AirMonitor.Data;
using Android.App;
using System;
using System.Linq;

namespace AirMonitor.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        public AirlyMesurements airlyMesurements;

        public DetailsViewModel()
        {
;
        }

        public void Init(AirlyMesurements airlyMesurements)
        {
            this.airlyMesurements = airlyMesurements;

            var index = airlyMesurements.Current.Indexes[0];
            CaqiValue = (int)Math.Round(index.Value ?? 0, 0);
            CaqiTitle = index.Description;
            CaqiDescription = index.Advice;

            var pm25Value = airlyMesurements.Current.Values.FirstOrDefault(x => x.Name == "PM25");
            var pm25Standard = airlyMesurements.Current.Standards.FirstOrDefault(x => x.Pollutant == "PM25");

            Pm25Value = (int)Math.Round(pm25Value != null ? pm25Value.Value : 0, 0);
            Pm25Percent = (int)Math.Round(pm25Standard != null ? pm25Standard.Percent : 0, 0);

            var pm10Value = airlyMesurements.Current.Values.FirstOrDefault(x => x.Name == "PM10");
            var pm10Standard = airlyMesurements.Current.Standards.FirstOrDefault(x => x.Pollutant == "PM10");

            Pm10Value = (int)Math.Round(pm10Value != null ? pm10Value.Value : 0, 0);
            Pm10Percent = (int)Math.Round(pm10Standard != null ? pm10Standard.Percent : 0, 0);

            var humidityValue = airlyMesurements.Current.Values.FirstOrDefault(x => x.Name == "HUMIDITY");
            var pressureValue = airlyMesurements.Current.Values.FirstOrDefault(x => x.Name == "PRESSURE");

            HumidityValue = humidityValue != null ? humidityValue.Value : 0;
            PressureValue = (int)Math.Round(pressureValue != null ? pressureValue.Value : 0, 0);
        }

        private int _caqiValue = 57;
        public int CaqiValue
        {
            get => _caqiValue;
            set => SetProperty(ref _caqiValue, value);
        }

        private string _caqiTitle = "Świetna jakość!";
        public string CaqiTitle
        {
            get => _caqiTitle;
            set => SetProperty(ref _caqiTitle, value);
        }

        private string _caqiDescription = "Możesz bezpiecznie wyjść z domu bez swojej maski anty-smogowej i nie bać się o swoje zdrowie.";
        public string CaqiDescription
        {
            get => _caqiDescription;
            set => SetProperty(ref _caqiDescription, value);
        }

        private int _pm25Value = 34;
        public int Pm25Value
        {
            get => _pm25Value;
            set => SetProperty(ref _pm25Value, value);
        }

        private int _pm25Percent = 137;
        public int Pm25Percent
        {
            get => _pm25Percent;
            set => SetProperty(ref _pm25Percent, value);
        }

        private int _pm10Value = 67;
        public int Pm10Value
        {
            get => _pm10Value;
            set => SetProperty(ref _pm10Value, value);
        }

        private int _pm10Percent = 135;
        public int Pm10Percent
        {
            get => _pm10Percent;
            set => SetProperty(ref _pm10Percent, value);
        }

        private double _humidityValue = 0.95;
        public double HumidityValue
        {
            get => _humidityValue;
            set => SetProperty(ref _humidityValue, value);
        }

        private int _pressureValue = 1027;
        public int PressureValue
        {
            get => _pressureValue;
            set => SetProperty(ref _pressureValue, value);
        }
    }
}