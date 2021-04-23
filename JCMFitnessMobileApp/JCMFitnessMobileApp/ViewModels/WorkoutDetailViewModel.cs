using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.LocalDB;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModels;
using MonkeyCache.FileStore;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModel
{
    public class WorkoutDetailViewModel : BaseViewModel<Workout>
    {
        Workout _workout;
        Exercise _exercise;
        private readonly INavService navService;
        readonly IFitnessService _fitnessService;
        private readonly ILocalDatabase localDatabase;
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

        public Exercise SelectedExercise {
            get => _exercise;
            set
            {
                _exercise = value;
                OnPropertyChanged();
            }
        }

        public WorkoutDetailViewModel(INavService navService, IFitnessService fitnessService, ILocalDatabase localDatabase)
            : base(navService)
        {
            Barrel.ApplicationId = "CachingDataSample";
            this.navService = navService;
            _fitnessService = fitnessService;
            this.localDatabase = localDatabase;
        }



        public override async void Init(Workout workout)
        {
            if(workout != null)
            {
                Barrel.Current.Add(key: "workout", data: workout, expireIn: TimeSpan.FromMinutes(5));
                Workout = workout;
                
            }

            WorkoutExercises = await LoadExercises(workout.WorkoutID);
        }


        public Command ExerciseSelectedCommand => new Command(async () =>
        {
            if (SelectedExercise != null)
            {
                await NavService.NavigateTo<ExerciseDetailViewModel, Exercise>(SelectedExercise);
                SelectedExercise = null;
            }

        }
  );

        Command _deleteCommand;
        public Command DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new Command(async () => await Delete()));

        Command _editCommand;
        public Command EditCommand =>
            _editCommand ?? (_editCommand = new Command(async () => await
            NavService.NavigateTo<EditWorkoutViewModel, Workout>(Workout)));

        public Command AddExerciseCommand =>
              new Command(async () =>
                  await NavService.NavigateTo<NewExerciseViewModel, Workout>(Workout));

      

        async Task<ObservableCollection<Exercise>> LoadExercises(string workoutid)
        {

            try
            {
                var workoutExercises = await _fitnessService.GetWorkoutExercises(workoutid);

                ObservableCollection<Exercise> newWorkouts = new ObservableCollection<Exercise>(workoutExercises);

                var LocalworkoutExercise = await localDatabase.GetWorkoutExercises(workoutid);

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
            var response = Barrel.Current.Get<LoginResponse>(key: "user");
            try
            {
                await _fitnessService.DeleteUserWorkoutById(response.User.Id, Workout.WorkoutID);
                await NavService.NavigateTo<MainViewModel>();
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
