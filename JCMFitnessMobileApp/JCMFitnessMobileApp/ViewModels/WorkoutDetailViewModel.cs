using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModels;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModel
{
    public class WorkoutDetailViewModel : BaseViewModel<Workout>
    {
        Workout _workout;
        readonly IFitnessService _fitnessService;


        ObservableCollection<Exercise> _workoutExercises;


        public ObservableCollection<Exercise> WorkoutExercises
        {
            get => _workoutExercises;
            set
            {
                _workoutExercises = value;
                OnPropertyChanged();
            }
        }

        public Workout Workout
        {
            get => _workout;
            set
            {
                _workout = value;
                OnPropertyChanged();
            }
        }

        public WorkoutDetailViewModel(INavService navService, IFitnessService fitnessService)
            : base(navService)
        {
            _fitnessService = fitnessService;
        }

        public override async void Init(Workout workout)
        {
            Workout = workout;
            WorkoutExercises = await LoadExercises(workout.WorkoutID);
        }

        Command _deleteCommand;
        public Command DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new Command(async () => await Delete()));

        Command _editCommand;
        public Command EditCommand =>
            _editCommand ?? (_editCommand = new Command(async () => await Edit()));

        public Command AddExerciseCommand =>
              new Command(async () =>
                  await NavService.NavigateTo<NewExerciseViewModel, Workout>(Workout));

        async Task<ObservableCollection<Exercise>> LoadExercises(string workoutid)
        {

            try
            {
                var workoutExercises = await _fitnessService.GetWorkoutExercises(workoutid);

                ObservableCollection<Exercise> newWorkouts = new ObservableCollection<Exercise>(workoutExercises);

                return new ObservableCollection<Exercise>(newWorkouts);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        async Task Delete()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await _fitnessService.DeleteUserWorkoutById("2", Workout.WorkoutID);
                await NavService.GoBack();
            }
            finally
            {
                IsBusy = false;
            }

        }

        async Task Edit()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                //await _fitnessService.DeleteUserWorkoutById("2", Workout.WorkoutID);
                await NavService.GoBack();
            }
            finally
            {
                IsBusy = false;
            }

        }





    }
}
