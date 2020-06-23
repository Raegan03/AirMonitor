using AirMonitor.Data;
using AirMonitor.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AirMonitor.Views
{
    [DesignTimeVisible(false)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Measurement  mesurements)
        {
            InitializeComponent();

            var detailsViewModel = new DetailsViewModel();
            detailsViewModel.Init(mesurements);

            BindingContext = detailsViewModel;
        }

        private void Help_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Co to jest CAQI?", "Lorem ipsum.", "Zamknij");
        }
    }
}
