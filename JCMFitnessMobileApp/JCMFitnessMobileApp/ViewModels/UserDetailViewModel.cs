﻿using System;
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
    
    public class UserDetailViewModel:BaseViewModel
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

        public void GetUserFromCache()
        {
            user = Barrel.Current.Get<User>(key: "user");
        }

        public async void DeleteUserAsync()
        {
                //bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
                //Debug.WriteLine("Answer: " + answer);
            

            //if (answer == true)
            //{
                await _fitnessService.DeleteUser(user.UserID);
            //}
            await NavService.NavigateTo<LoginViewModel>();
            
        }

        
    }
}
