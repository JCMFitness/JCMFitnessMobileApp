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

namespace JCMFitnessMobileApp
{
    public partial class App : Application
    {
        //bool IsSignedIn => !string.IsNullOrWhiteSpace(Preferences.Get("apitoken", ""));

        public IKernel Kernel { get; set; }
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();

            /* var mainPage = new NavigationPage(new MainPage());
             var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;
             navService.XamarinFormsNav = mainPage.Navigation;
             MainPage = mainPage;*/


            // Register core services
            Kernel = new StandardKernel(
                new CoreModule(),
                new TripLogNavModule());
            // Register platform specific services
            Kernel.Load(platformModules);

            // Setup data service authentication delegates
            var dataService = Kernel.Get<IFitnessService>();
            //dataService.AuthorizedDelegate = OnSignIn;
            //MainPage = new AppShell();
            SetMainPage();
        }


        void SetMainPage()
        {
            /*var mainPage = IsSignedIn
             ? new NavigationPage(new MainPage())
             {
                 BindingContext = Kernel.Get<MainViewModel>()
             }
             : new NavigationPage(new SignInPage())
             {
                 BindingContext = Kernel.Get<SignInViewModel>()
             };*/


            var mainPage = new NavigationPage(new LandingPage())
            {
                BindingContext = Kernel.Get<LandingViewModel>()
            };

            var navService = Kernel.Get<INavService>() as XamarinFormsNavService;
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
