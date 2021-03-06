﻿using AirMonitor.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            var homeViewModel = new HomeViewModel(Navigation);
            homeViewModel.CheckForData();

            BindingContext = homeViewModel;
        }

        private void ItemTappedHandle(object sender, ItemTappedEventArgs e)
        {
            (BindingContext as HomeViewModel)?.GoToDetailsCommand.Execute(e.ItemIndex);
        }
    }
}