using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace JCMFitnessMobileApp.ViewModels
{
    public class EditViewModel : BaseViewModel
    {
        public Workout _workout;
        readonly IFitnessService _fitnessService;
        public EditViewModel(INavService navService, IFitnessService fitnessService)
           : base(navService)
        {
            _fitnessService = fitnessService;
        }

        public override async void Init(Workout workout)
        {
            _workout = workout;
        }

    }
}
