using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using MonkeyCache.FileStore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Timers;
using System.Threading;

namespace JCMFitnessMobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel<User>
    {
        private IFitnessService _fitnessService;
        private static System.Timers.Timer timer;
        private static int timerCounter;

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

        public DelegateCommand SignInCommand { get; private set; }
        public DelegateCommand ValidateLoginCommand { get; private set; }
        public DelegateCommand ValidatePwdCommand { get; private set; }


        public LoginViewModel(INavService navService, IFitnessService fitService)
            : base(navService)
        {
            _fitnessService = fitService;
            Barrel.ApplicationId = "CachingDataSample";
            SignInCommand = new DelegateCommand(SignIp, CanExecuteSignIn);
            ValidateLoginCommand = new DelegateCommand(ValidateLogin, () => UserName != null);
            ValidatePwdCommand = new DelegateCommand(ValidatePwd, () => UserName != null);
        }
        private void SignIp()
        {
            // Call Service for login.
        }

        private bool CanExecuteSignIn()
        {
            if (UserName == null || Password == null)
            {
                return false;
            }

            if (!IsLoginInValid && !IsPwdInValid)
            {
                return true;
            }
            return false;
        }

        private bool isLoginInValid;
        public bool IsLoginInValid
        {
            get => isLoginInValid;
            set
            {
                isLoginInValid = value;
                OnPropertyChanged();
            }
        }

        private bool isPwdInValid;
        public bool IsPwdInValid
        {
            get => isPwdInValid;
            set
            {
                isPwdInValid = value;
                OnPropertyChanged();
            }
        }

      

        private void ValidateLogin()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(UserName);
            IsLoginInValid = !match.Success;
            SignInCommand.RaiseCanExecuteChanged();
        }

        private void ValidatePwd()
        {
            IsPwdInValid = string.IsNullOrEmpty(Password);
            SignInCommand.RaiseCanExecuteChanged();
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

        /*public override void Init()
        {
            NavService.ClearBackStack();
        }*/



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

                //User = Barrel.Current.Get<User>(key: "user");

                if (User == null)
                {

                    var userLogin = new UserLogin
                    {
                        UserName = UserName,
                        Password = Password

                    };


                    try
                    {
                        var loginResponse = await _fitnessService.LoginUser(userLogin);
                        Barrel.Current.Add(key: "user", data: loginResponse, expireIn: TimeSpan.FromMinutes(1));
                        Vibration.Vibrate(50);
                        await Task.Run(() => Thread.Sleep(2500));
                        await NavService.NavigateTo<MainViewModel>();
                    }
                    catch
                    {
                        FailedVibration();
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct username or password", "OK");
                        await NavService.NavigateTo<LoginViewModel>();
                    }

                }

               

               //NavService.RemoveLastView();
            }
            catch 
            {
                throw;
            }

 
            //await NavService.NavigateTo<MainViewModel, User>(user);
        }

        void FailedVibration()
        {
            timerCounter = 0;
            timer = new System.Timers.Timer(500);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;    
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (timerCounter == 2)
            {
                timer.Stop();
            }
            else
            {
                Vibration.Vibrate(50);
            }
            timerCounter++;
            
        }
    }
}
