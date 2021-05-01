using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModels
{
    public class NewExerciseViewModel : BaseViewModel<Workout>
    {

        readonly IFitnessService _fitnessService;

        public Workout _workout;

        public NewExerciseViewModel(INavService navService, IFitnessService fitness)
            : base(navService)
        {
            _fitnessService = fitness;
        }

        public override void Init(Workout workout)
        {
            _workout = workout;
        }

        string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                //Validate(() => !string.IsNullOrWhiteSpace(_name), "Name must be provided.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }


        int _timerValue;
        public int TimerValue
        {
            get => _timerValue;
            set
            {
                _timerValue = value;
                OnPropertyChanged();
            }
        }


        int _repetitions;
        public int Repetitions
        {
            get => _repetitions;
            set
            {
                _repetitions = value;
                OnPropertyChanged();
            }
        }


        int _sets;
        public int Sets
        {
            get => _sets;
            set
            {
                _sets = value;
                OnPropertyChanged();
            }
        }

        Command _saveCommand;
        public Command SaveCommand =>
            _saveCommand ?? (_saveCommand = new Command(async () => await Save()));

        async Task Save()
        {

            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                await Task.Run(() => Thread.Sleep(5000));
                var newExercise = new Exercise
                {
                    ExerciseID = Guid.NewGuid().ToString(),
                    Name = Name,
                    TimerValue = TimerValue,
                    Repititions = Repetitions,
                    Sets = Sets,
                    IsPublic = false

                };
                await _fitnessService.AddWorkoutExercise(_workout.WorkoutID, newExercise);

                //NavService.RemoveLastView();
                NavService.RemoveLastTwoViews();
                //NavService.ClearBackStack();
                
                await NavService.NavigateTo<WorkoutDetailViewModel, Workout>(_workout);
            }
            finally
            {
                IsBusy = false;
            }

        }
       // bool CanSave() => !string.IsNullOrWhiteSpace(Name) && !HasErrors;



    }
}
