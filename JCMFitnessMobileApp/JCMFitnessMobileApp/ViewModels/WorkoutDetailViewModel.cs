using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModels;
using MonkeyCache.FileStore;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModel
{
    public class WorkoutDetailViewModel : BaseViewModel<Workout>
    {
        Workout _workout;
        Exercise _exercise;
        readonly IFitnessService _fitnessService;
        private static Timer timer;
        private static int timerCounter;

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

        public WorkoutDetailViewModel(INavService navService, IFitnessService fitnessService)
            : base(navService)
        {
            Barrel.ApplicationId = "CachingDataSample";
            _fitnessService = fitnessService;
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
                var answer = await App.Current.MainPage.DisplayAlert("Are You Sure?", "Are you sure you want to delete this workout?", "No", "Yes");
                if (answer == false)
                {
                    await _fitnessService.DeleteUserWorkoutById(response.User.Id, Workout.WorkoutID);
                    Vibration.Vibrate(50);
                    await NavService.NavigateTo<MainViewModel>();
                }
                else
                {
                    FailedVibration();
                    return;
                }

            }
            catch
            {
                FailedVibration();
            }
            finally
            {
                IsBusy = false;
            }

        }
        void FailedVibration()
        {
            timerCounter = 0;
            timer = new Timer(500);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (timerCounter == 2)
            {
                timer.Stop();
            }
            else
            {
                Vibration.Vibrate(50);
            }
            timerCounter++;

        }

    }
}
