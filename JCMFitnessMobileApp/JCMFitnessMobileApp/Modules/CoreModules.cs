using JCMFitnessMobileApp.ViewModel;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using JCMFitnessMobileApp.ViewModel;
using Xamarin.Essentials;
using JCMFitnessMobileApp.Services;
using Refit;

namespace JCMFitnessMobileApp.Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            // ViewModels 
            //Bind<SignInViewModel>().ToSelf();
            Bind<MainViewModel>().ToSelf();
            Bind<WorkoutDetailViewModel>().ToSelf();
            Bind<NewWorkoutViewModel>().ToSelf();

            var baseUrl = "https://jcmfitnessapi.herokuapp.com";

            //var apiAuthToken = Preferences.Get("apitoken", "");

            IFitApi refitInstance = RestService.For<IFitApi>(baseUrl);

          
            var tripLogService = new FitnessService(refitInstance);

            

            /*var fitApiService = new IFitApi();

            containerBuilder.RegisterInstance(refitInstance)
                .As<IFitApi>();*/

            Bind<Akavache.IBlobCache>().ToConstant(Akavache.BlobCache.LocalMachine);

            //Bind<IAuthService>().To<AuthService>().InSingletonScope();

            Bind<IFitnessService>()
                .ToMethod(x => tripLogService)
                .InSingletonScope();

        }
    }
}
