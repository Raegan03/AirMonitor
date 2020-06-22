using AirMonitor.Data;
using AirMonitor.Services;
using AirMonitor.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        private ObservableCollection<AirlyInstallation> _airlyInstallations = 
            new ObservableCollection<AirlyInstallation>();
        public ObservableCollection<AirlyInstallation> AirlyInstallations
        {
            get => _airlyInstallations;
            set => SetProperty(ref _airlyInstallations, value);
        }

        private bool _fetchingData;
        public bool FetchingData
        {
            get => _fetchingData;
            set => SetProperty(ref _fetchingData, value);
        }

        private ICommand _goToDetailsCommand;
        public ICommand GoToDetailsCommand => _goToDetailsCommand ??
            (_goToDetailsCommand = new Command<int>(OnGoToDetails));

        private async void OnGoToDetails(int index)
        {
            FetchingData = true;
            var measurements = await AirlyService.TryGetAirlyMeasurements(AirlyInstallations[index].Id);
            FetchingData = false;

            await _navigation.PushAsync(new DetailsPage(measurements));
        }

        private ICommand _airlyTestCommand;
        public ICommand AirlyTestCommand => _airlyTestCommand ??
            (_airlyTestCommand = new Command(AirlyTest));

        private async void AirlyTest()
        {
            FetchingData = true;
            AirlyInstallations = new ObservableCollection<AirlyInstallation>(
                await AirlyService.TryGetAirlyInstallations());
            FetchingData = false;
        }
    }
}