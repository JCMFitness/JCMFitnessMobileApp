using JCMFitnessMobileApp.LocalDB;
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
using System.Windows.Input;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using JCMFitnessMobileApp.MyConstants;

namespace JCMFitnessMobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel<User>
    {
        private readonly INavService navService;
        private IFitnessService _fitnessService;
        private ILocalDatabase _localDatabase;
        private readonly ISyncService syncService;
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


        public LoginViewModel(INavService navService, IFitnessService fitService, ILocalDatabase localDatabase, ISyncService syncService)
            : base(navService)
        {
            this.navService = navService;
            _fitnessService = fitService;
            _localDatabase = localDatabase;
            this.syncService = syncService;
            Barrel.ApplicationId = "CachingDataSample";
            SignInCommand = new DelegateCommand(SignIp, CanExecuteSignIn);
            ValidateLoginCommand = new DelegateCommand(ValidateLogin, () => UserName != null);
            ValidatePwdCommand = new DelegateCommand(ValidatePwd, () => UserName != null);
            GoogleCommand = new Command(async () => await OnAuthenticate("Google"));
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
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        FailedVibration();
                        await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct username or password", "OK");
                        IsBusy = false;
                    }

                }

               

               NavService.RemoveLastTwoViews();
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
        const string authenticationUrl = "https://jcmfitness1.azurewebsites.net/api/authentication/";
        //const string authenticationUrl = "https://googleauth2.azurewebsites.net/mobileauth/";

        private JsonSerializer _serializer = new JsonSerializer();
        public ICommand GoogleCommand { get; }




        string accessToken = string.Empty;



        string _authToken;
        public string AuthToken
        {
            get => _authToken;
            set
            {
                _authToken = value;
                OnPropertyChanged();
            }
        }

        async Task OnAuthenticate(string scheme)
        {
            try
            {
                WebAuthenticatorResult r = null;

                var authUrl = new Uri(authenticationUrl + scheme);
                var callbackUrl = new Uri("xamarinessentials://");

                r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                var loginResponse = new LoginResponse
                {
                    Token = r.Properties["access_token"],
                    ValidTo = r.Properties["expires"],
                    Email = r.Properties["email"],
                    UserId = r.Properties["id"]
                };

                Barrel.Current.Add(key: "user", data: loginResponse, expireIn: TimeSpan.FromMinutes(10));

                AuthToken = r?.AccessToken ?? r?.IdToken;
                GetUserInfoUsingToken(AuthToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AuthToken = string.Empty;
                await App.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct username or password", "OK");
            }
        }
        private async void GetUserInfoUsingToken(string authToken)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.googleapis.com/oauth2/v3/");
            var httpResponseMessage = await httpClient.GetAsync("tokeninfo?access_token=" + authToken);
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                var jsoncontent = _serializer.Deserialize<GoogleResponseModel>(json);
                Preferences.Set("UserToken", authToken);
                //Not  a best way to save auth token and check if authtoken has expired insted try implementing refresh token
                await NavService.NavigateTo<MainViewModel>();
            }

        }


    }
}
