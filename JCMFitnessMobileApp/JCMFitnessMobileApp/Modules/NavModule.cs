using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using JCMFitnessMobileApp.ViewModels;
using JCMFitnessMobileApp.Views;
using Ninject.Modules;
using System;
using System.Collections.Generic;


namespace JCMFitnessMobileApp.Modules
{
    public class TripLogNavModule : NinjectModule
    {
        public override void Load()
        {
            var navService = new XamarinFormsNavService();

            //navService.RegisterViewMapping(typeof(SignInViewModel), typeof(SignInPage));
            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(WorkoutDetailViewModel), typeof(WorkoutDetailPage));
            navService.RegisterViewMapping(typeof(NewWorkoutViewModel), typeof(NewWorkoutPage));
            navService.RegisterViewMapping(typeof(LoginViewModel), typeof(LoginPage));

            Bind<INavService>()
            .ToMethod(x => navService)
            .InSingletonScope();
        }
    }
}
