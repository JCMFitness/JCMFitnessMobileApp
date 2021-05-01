using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.MyConstants;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        

        const string authenticationUrl = "https://jcmfitness1.azurewebsites.net/api/authentication/";
        //const string authenticationUrl = "https://googleauth2.azurewebsites.net/mobileauth/";


        private JsonSerializer _serializer = new JsonSerializer();

        public LandingViewModel(INavService navService)
        : base(navService)
        {
            GoogleCommand = new Command(async () => await OnAuthenticate("Google"));
            Barrel.ApplicationId = "CachingDataSample";
        }

        public override void Init()
        {
            NavService.ClearBackStack();
        }

        public Command LoginCommand =>
          new Command(async () =>
              await NavService.NavigateTo<LoginViewModel>());

        public Command SignupCommand =>
        new Command(async () =>
            await NavService.NavigateTo<SignupViewModel>());


        

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
