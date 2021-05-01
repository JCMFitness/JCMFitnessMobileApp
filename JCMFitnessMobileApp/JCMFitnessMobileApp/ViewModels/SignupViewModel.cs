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
    public class SignupViewModel:BaseViewModel
    {

        private IFitnessService _fitnessService;

        public SignupViewModel(INavService navService, IFitnessService fitService)
       : base(navService)
        {
            _fitnessService = fitService;
        }



        string _userName;
        public string Username
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
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

        string _firstname;
        public string FirstName
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged();
            }
        }


        string _lastname;
        public string LastName
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged();
            }
        }






        Command _SignUpCommand;
        public Command SignUpCommand =>
            _SignUpCommand ?? (_SignUpCommand = new Command(async () => await SignUpUser()));



        public async Task SignUpUser()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {

                var userSignUp = new UserSignUp
                {
                    UserName = Username,
                    Email = Email,
                    Password = Password,
                    FirstName = FirstName,
                    LastName = LastName
                };


                try
                {
                    await _fitnessService.SignUpUser(userSignUp);
                    await NavService.NavigateTo<LoginViewModel>();
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Sign up Fail", "Please enter valid username or password", "OK");
                    IsBusy = false;
                }

                IsBusy = false;

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
