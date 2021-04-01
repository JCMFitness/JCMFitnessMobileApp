using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModels
{
    public class LoginViewModel : BaseValidationViewModel
    {
        private IFitnessService _fitnessService;

        public User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }


        public LoginViewModel(INavService navService, IFitnessService fitService)
            : base(navService)
        {
            _fitnessService = fitService;

        }

        string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }


        string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        

        Command _LoginCommand;
        public Command LoginCommand =>
            _LoginCommand ?? (_LoginCommand = new Command(async () => await LoginUser()));
        public async Task LoginUser()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {


                // Username and password needs to be entered by the user here

                User = await _fitnessService.LoginUser(UserName, Password);
                IsBusy = false;

                await NavService.NavigateTo<MainViewModel, User>(User);

            }
            catch
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
            //await NavService.NavigateTo<MainViewModel, User>(user);
        }
    }
}
