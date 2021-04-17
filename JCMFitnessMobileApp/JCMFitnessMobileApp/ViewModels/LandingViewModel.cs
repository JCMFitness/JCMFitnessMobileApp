using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.MyConstants;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
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
    public class LandingViewModel:BaseViewModel
    {
        //const string authenticationUrl = "https://tranquil-bastion-01081.herokuapp.com/mobileauth/";
        const string authenticationUrl = "https://xamarin-essentials-auth-sample.azurewebsites.net/mobileauth/";
        private JsonSerializer _serializer = new JsonSerializer();

        public LandingViewModel(INavService navService)
        : base(navService)
        {
            MicrosoftCommand = new Command(async () => await OnAuthenticate("Microsoft"));
            GoogleCommand = new Command(async () => await OnAuthenticate("Google"));
            FacebookCommand = new Command(async () => await OnAuthenticate("Facebook"));
           
        }


        public Command LoginCommand =>
          new Command(async () =>
              await NavService.NavigateTo<LoginViewModel>());

        public Command SignupCommand =>
        new Command(async () =>
            await NavService.NavigateTo<SignupViewModel>());


        public ICommand MicrosoftCommand { get; }

        public ICommand GoogleCommand { get; }

        public ICommand FacebookCommand { get; }


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
