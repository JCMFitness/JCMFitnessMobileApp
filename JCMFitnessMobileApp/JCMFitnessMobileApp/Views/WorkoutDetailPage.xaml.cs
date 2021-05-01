using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Diagnostics;
using JCMFitnessMobileApp.ViewModel;
using System.ComponentModel;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModels;

namespace JCMFitnessMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutDetailPage : ContentPage
    {
        WorkoutDetailViewModel ViewModel => BindingContext as WorkoutDetailViewModel;
        public WorkoutDetailPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.RefreshExercisesOnAppearing();
        }
    }
}