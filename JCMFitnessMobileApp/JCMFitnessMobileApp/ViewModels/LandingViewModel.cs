using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModels
{
    public class LandingViewModel:BaseViewModel
    {

        public LandingViewModel(INavService navService)
        : base(navService)
        {
           
        }


        public Command LoginCommand =>
          new Command(async () =>
              await NavService.NavigateTo<LoginViewModel>());

        public Command SignupCommand =>
        new Command(async () =>
            await NavService.NavigateTo<SignupViewModel>());
    }
}
