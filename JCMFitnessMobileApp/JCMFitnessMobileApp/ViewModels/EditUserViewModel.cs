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
    public class EditUserViewModel: BaseViewModel<User>
    {
        User _user;
        readonly IFitnessService _fitnessService;
        public EditUserViewModel(INavService navService, IFitnessService fitnessService)
           : base(navService)
        {
            _fitnessService = fitnessService;
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
                await _fitnessService.EditUser(User);
                await NavService.NavigateTo<UserDetailViewModel>();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
