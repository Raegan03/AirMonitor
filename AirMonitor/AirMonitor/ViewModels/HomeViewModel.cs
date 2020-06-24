using AirMonitor.Data;
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

        public void CheckForData()
        {
            AirlyInstallations = new ObservableCollection<Installation>(App.DataHelper.AirlyInstallations);
            EmptyData = AirlyInstallations.Count == 0;
        }

        private ObservableCollection<Installation> _airlyInstallations = 
            new ObservableCollection<Installation>();
        public ObservableCollection<Installation> AirlyInstallations
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

        private bool _emptyData;
        public bool EmptyData
        {
            get => _emptyData;
            set => SetProperty(ref _emptyData, value);
        }

        private ICommand _goToDetailsCommand;
        public ICommand GoToDetailsCommand => _goToDetailsCommand ??
            (_goToDetailsCommand = new Command<int>(OnGoToDetails));

        private async void OnGoToDetails(int index)
        {
            var measurements = App.DataHelper.GetMeasurement(AirlyInstallations[index].Id);
            await _navigation.PushAsync(new DetailsPage(measurements));
        }

        private ICommand _fetchDataCommand;
        public ICommand FetchDataCommand => _fetchDataCommand ??
            (_fetchDataCommand = new Command(OnFetchData));

        private async void OnFetchData()
        {
            FetchingData = true;

            await App.DataHelper.TryFetchData();
            AirlyInstallations = new ObservableCollection<Installation>(App.DataHelper.AirlyInstallations);

            EmptyData = AirlyInstallations.Count == 0;

            FetchingData = false;
        }
    }
}