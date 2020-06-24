using AirMonitor.Data;
using AirMonitor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirMonitor.Helpers
{
    public class DataHelper : IDisposable
    {
        public IReadOnlyList<Installation> AirlyInstallations => _airlyInstallations;
        private List<Installation> _airlyInstallations;

        public IReadOnlyList<Measurement> AirlyMeasurements => _airlyMeasurements;
        private List<Measurement> _airlyMeasurements;

        public async Task InitData()
        {
            _airlyInstallations = await App.DatabaseHelper.GetInstallations();
            _airlyMeasurements = await App.DatabaseHelper.GetMeasurements();
        }

        public async Task TryFetchData()
        {
            bool saveData = false;
            if (_airlyInstallations.Count == 0)
            {
                await FetchDataFromAirly();

                saveData = true;
            }
            else
            {
                var currentDateTime = DateTime.UtcNow;
                for (int i = 0; i < _airlyMeasurements.Count; i++)
                {
                    var measurement = _airlyMeasurements[i];
                    if (measurement.Current.TillDateTime < currentDateTime)
                    {
                        var installation = measurement.Installation;

                        var newMeasurement = await AirlyService.GetMeasurementFromAirly(installation.Id);
                        newMeasurement.Installation = installation;

                        _airlyMeasurements[i] = newMeasurement;

                        saveData = true;
                    }
                }
            }

            if(saveData)
            {
                await SaveDataInDatabase();
            }
        }

        public Measurement GetMeasurement(string installationId)
            => _airlyMeasurements.FirstOrDefault(x => x.Installation.Id == installationId);

        private async Task FetchDataFromAirly()
        {
            (var installations, var measurements) = await AirlyService.GetDataFromAirly();

            _airlyInstallations = installations;
            _airlyMeasurements = measurements;
        }

        private async Task SaveDataInDatabase()
        {
            await App.DatabaseHelper.SaveInstallations(_airlyInstallations);
            await App.DatabaseHelper.SaveMeasurements(_airlyMeasurements);
        }

        public void Dispose()
        {
            _airlyInstallations = null;
            _airlyMeasurements = null;
        }
    }
}
