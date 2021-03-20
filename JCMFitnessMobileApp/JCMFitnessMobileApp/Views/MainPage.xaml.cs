using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JCMFitnessMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        MainViewModel ViewModel => BindingContext as MainViewModel;
        // ...
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Initialize MainViewModel
            ViewModel?.Init();
        }

        public MainPage()
        {
            InitializeComponent();
           
        }

        void New_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewEntryPage());
        }

        async void Trips_SelectionChanged(object s, SelectionChangedEventArgs e)
        {
            var trip = (TripLogEntry)e.CurrentSelection.FirstOrDefault();
            if (trip != null)
            {
                await Navigation.PushAsync(new DetailPage());
            }
            // Clear selection 
            trips.SelectedItem = null;
        }

       
    }
}