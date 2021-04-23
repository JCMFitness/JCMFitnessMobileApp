using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JCMFitnessMobileApp.Services;
using Ninject.Modules;
using Ninject;
using JCMFitnessMobileApp.Modules;
using Xamarin.Essentials;
using JCMFitnessMobileApp.Views;
using JCMFitnessMobileApp.ViewModel;
using JCMFitnessMobileApp.ViewModels;
using MonkeyCache.FileStore;
using JCMFitnessMobileApp.Models;
using Plugin.FirebasePushNotification;

namespace JCMFitnessMobileApp
{
    public partial class App : Application
    {
       

        public IKernel Kernel { get; set; }
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            Barrel.ApplicationId = "CachingDataSample";

            // Register core services
            Kernel = new StandardKernel(
                new CoreModule(),
                new TripLogNavModule());
            // Register platform specific services
            Kernel.Load(platformModules);

            // Setup data service authentication delegates
            var dataService = Kernel.Get<IFitnessService>();
          
            SetMainPage();

         
        }


        

        void SetMainPage()
        {

            bool IsSignedIn = Barrel.Current.Exists("user");
            var navService = Kernel.Get<INavService>() as XamarinFormsNavService;
            var mainPage = IsSignedIn
             ? new NavigationPage(new MainPage())
             {
                 BindingContext = Kernel.Get<MainViewModel>()
             }
             : new NavigationPage(new LandingPage())
             {
                 BindingContext = Kernel.Get<LandingViewModel>()
             };


            /*var mainPage = new NavigationPage(new LandingPage())
            {
                BindingContext = Kernel.Get<LandingViewModel>()
            };*/


            navService.XamarinFormsNav = mainPage.Navigation;
            MainPage = mainPage;
        }


       /* void OnSignIn(string accessToken)
        {
            Preferences.Set("apitoken", accessToken);
            SetMainPage();
        }*/

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
