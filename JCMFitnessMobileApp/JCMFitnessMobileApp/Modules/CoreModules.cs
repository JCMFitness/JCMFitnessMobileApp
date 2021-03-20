using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCMFitnessMobileApp.Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            // ViewModels 
            //Bind<SignInViewModel>().ToSelf();
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();


            var apiAuthToken = Preferences.Get("apitoken", "");

            // Core Services
            var tripLogService = new TripLogApiDataService(new Uri("https://TripLogAppMaksad.azurewebsites.net"), apiAuthToken);

            Bind<Akavache.IBlobCache>().ToConstant(Akavache.BlobCache.LocalMachine);

            Bind<IAuthService>().To<AuthService>().InSingletonScope();

            Bind<ITripLogDataService>()
                .ToMethod(x => tripLogService)
                .InSingletonScope();
        }
    }
}
