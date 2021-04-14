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
    public class ExerciseDetailViewModel : BaseViewModel<Exercise>
    {
        readonly IFitnessService _fitnessService;
        Exercise _Exer;
        

        public Exercise Exercise
        {
            get => _Exer;

            set
            {
                _Exer = value;
                OnPropertyChanged();
            }
        }
        public ExerciseDetailViewModel(INavService navService, IFitnessService fitnessService)
    : base(navService)
        {
            _fitnessService = fitnessService;
        }

        public override void Init(Exercise exercise)
        {
            Exercise = exercise;
        }

        Command _editCommand;
        public Command EditCommand =>
        _editCommand ?? (_editCommand = new Command(async () => await
        NavService.NavigateTo<EditExerciseViewModel, Exercise>(Exercise)));

        Command _deleteExerciseCommand;
        public Command DeleteExerciseCommand =>
        _deleteExerciseCommand ?? (_deleteExerciseCommand = new Command(async () => await DeleteExercise()));

        async Task DeleteExercise()
        {
            await _fitnessService.DeleteExercise(Exercise.ExerciseID);
        }
    }
}
