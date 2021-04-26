using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.RefreshWorkoutsOnAppearing();
        }

        public MainPage()
        {
            InitializeComponent();
            //MainPage.HasBackButton(this, false);
        }

        void New_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewWorkoutPage());
        }

        async void Trips_SelectionChanged(object s, SelectionChangedEventArgs e)
        {
            var trip = (Workout)e.CurrentSelection.FirstOrDefault();
            if (trip != null)
            {
                await Navigation.PushAsync(new WorkoutDetailPage());
            }
            // Clear selection 
            trips.SelectedItem = null;
        }

       
    }
}