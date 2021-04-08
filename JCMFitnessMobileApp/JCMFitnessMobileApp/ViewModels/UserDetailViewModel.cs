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


namespace JCMFitnessMobileApp.ViewModels
{
    
    class UserDetailViewModel
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
        public string UserUserId;
        public string UserFirstName;
        public string UserLastName;
        public string UserUserName;
        public string UserEmail;
        public DateTime UserJoinedDate;

        User = Barrel.Current.Get<User>(key: "user");
    }
}
