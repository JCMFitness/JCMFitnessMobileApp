using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Diagnostics;
using JCMFitnessMobileApp.ViewModel;
using System.ComponentModel;
using Xamarin.Forms;
using JCMFitnessMobileApp.Models;
using MonkeyCache.FileStore;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Services;

namespace JCMFitnessMobileApp.ViewModels
{

    public class UserDetailViewModel : BaseViewModel
    {
        Command _deleteUser;
        public Command DeleteUserCommand =>
            _deleteUser ?? (_deleteUser = new Command(DeleteUserAsync));
        public User _user;
        readonly IFitnessService _fitnessService;

        public User user
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public UserDetailViewModel(INavService navService, IFitnessService fitnessService)
        : base(navService)
        {

            _fitnessService = fitnessService;
            Barrel.ApplicationId = "CachingDataSample";
        }

        public override void Init()
        {

            GetUserFromCache();
        }

        Command _editUserCommand;
        public Command EditUserCommand =>
        _editUserCommand ?? (_editUserCommand = new Command(async () => await
        NavService.NavigateTo<EditUserViewModel, User>(user)));

        public void GetUserFromCache()
        {
            user = Barrel.Current.Get<LoginResponse>(key: "user").User;
        }

        public async void DeleteUserAsync()
        {
            var answer = await App.Current.MainPage.DisplayAlert("Are You Sure?", "Are you sure you want to delete this account?", "No", "Yes");
            if(answer == false)
            {
                await _fitnessService.DeleteUser(user.Id);
                NavService.ClearBackStack();
                await NavService.NavigateTo<LoginViewModel>();
            }

        }

        public async void EditUserAsync()
        {
            await NavService.NavigateTo<UserDetailViewModel>();
        }

    }
}
