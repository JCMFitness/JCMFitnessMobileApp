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
    
    public class UserDetailViewModel:BaseViewModel
    {
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

            public UserDetailViewModel(INavService navService)
            : base(navService)
        {
            

            Barrel.ApplicationId = "CachingDataSample";
        }

        public override void Init()
        {
          
            GetUserFromCache();
        }

        public void GetUserFromCache()
        {
            User = Barrel.Current.Get<User>(key: "user");
        }
    }
}
