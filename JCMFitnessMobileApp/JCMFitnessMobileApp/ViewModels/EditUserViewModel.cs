using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModels
{
    public class EditUserViewModel: BaseViewModel<User>
    {
        User _user;
        readonly IFitnessService _fitnessService;
        public EditUserViewModel(INavService navService, IFitnessService fitnessService)
           : base(navService)
        {
            _fitnessService = fitnessService;
            Barrel.ApplicationId = "CachingDataSample";
        }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public override void Init(User user)
        {
            User = user;
        }

        Command _saveUserEditCommand;
        public Command SaveUserCommand =>
            _saveUserEditCommand ?? (_saveUserEditCommand = new Command(async () => await EditUser()));


        async Task EditUser()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                User.NormalizedUserName = User.UserName.ToUpper();
                User.NormalizedEmail = User.Email.ToUpper();

                await _fitnessService.EditUser(User);

                var loginResponse = Barrel.Current.Get<LoginResponse>(key: "user");
                loginResponse.User = User;
                Barrel.Current.Add(key: "user", data: loginResponse, expireIn: TimeSpan.FromMinutes(10));

                NavService.RemoveLastTwoViews();
                await NavService.NavigateTo<UserDetailViewModel>();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
