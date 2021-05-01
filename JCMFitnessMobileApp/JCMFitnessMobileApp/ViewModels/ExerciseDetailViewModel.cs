using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JCMFitnessMobileApp.ViewModels
{
    public class ExerciseDetailViewModel : BaseViewModel<Exercise>
    {
        readonly IFitnessService _fitnessService;
        Exercise _Exer;
        private static Timer timer;
        private static int timerCounter;

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
            Barrel.ApplicationId = "CachingDataSample";
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
            if (IsBusy)
                return;
            IsBusy = true;


            var answer = await App.Current.MainPage.DisplayAlert("Are You Sure?", "Are you sure you want to delete this exercise?", "No", "Yes");
            if (answer == false)
            {
                try
                {
                    var workout = Barrel.Current.Get<Workout>(key: "workout");

                    await _fitnessService.DeleteWorkoutExercise(workout.WorkoutID, Exercise.ExerciseID);

                    NavService.RemoveLastTwoViews();
                    await NavService.NavigateTo<WorkoutDetailViewModel, Workout>(workout);
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
            else
            {
                FailedVibration();
                return;
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
