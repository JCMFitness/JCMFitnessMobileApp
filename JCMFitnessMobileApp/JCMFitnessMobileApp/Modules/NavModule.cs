using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCMFitnessMobileApp.Modules
{
    public class TripLogNavModule : NinjectModule
    {
        public override void Load()
        {
        /*    var navService = new XamarinFormsNavService();

            navService.RegisterViewMapping(typeof(SignInViewModel), typeof(SignInPage));
            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
            navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));

            Bind<INavService>()
            .ToMethod(x => navService)
            .InSingletonScope();*/
        }
    }
}
