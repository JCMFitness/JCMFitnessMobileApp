using Akavache;
using FluentAssertions;
using JCMFitnessMobileApp.LocalDB;
using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Models.ApiDB;
using JCMFitnessMobileApp.Services;
using JCMFitnessMobileApp.ViewModel;
using JCMFitnessMobileApp.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessAppTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestingUserWorkouts()
        {
            var newsMock = new Mock<IFitApi>();

            var ExpectedWorkouts = new List<Workout>()
            {
                new Workout { WorkoutID = "1", Name = "Run",
                    Description = "Short description here", Category = "Sport"},
                new Workout { WorkoutID = "2", Name = "Walk",
                    Description = "Short description here", Category = "Sport"},
                new Workout { WorkoutID = "3", Name = "Sleep",
                    Description = "Short description here", Category = "Sport"},
                new Workout { WorkoutID = "4", Name = "Eat",
                    Description = "Short description here", Category = "Sport"},

            };

            var fitnessService = new FitnessService(newsMock.Object);

            var NavService = new Mock<INavService>();
            var BlobCache = new Mock<IBlobCache>();
            var LocalDB = new Mock<ILocalDatabase>();

            var SyncService = new Mock<ISyncService>();


            newsMock.Setup(m => m.GetUserWorkoutsAsync("1")).ReturnsAsync(ExpectedWorkouts);


            var vm = new MainViewModel(NavService.Object, fitnessService, BlobCache.Object, LocalDB.Object, SyncService.Object);

            await vm.LoadEntriesAsync();

            vm.UserWorkouts.Should().BeEquivalentTo(ExpectedWorkouts);

        }

        [Test]
        public async Task TestingWorkoutExercises()
        {
            var newsMock = new Mock<IFitApi>();
            var fitnessService = new FitnessService(newsMock.Object);

            var NavService = new Mock<INavService>();
            var BlobCache = new Mock<IBlobCache>();
            var LocalDB = new Mock<ILocalDatabase>();

            var SyncService = new Mock<ISyncService>();

            var ExpectedExercises = new List<Exercise>()
            {
                new Exercise { ExerciseID = "1", Name = "Run ex1"},
                new Exercise { ExerciseID = "2", Name = "Run ex2"},
                new Exercise { ExerciseID = "3", Name = "Run ex3"},
                new Exercise { ExerciseID = "4", Name = "Run ex4"},
                new Exercise { ExerciseID = "5", Name = "Run ex5"},
                new Exercise { ExerciseID = "6", Name = "Run ex6"}
            };

            newsMock.Setup(m => m.GetWorkoutExercisesAsync("1")).ReturnsAsync(ExpectedExercises);

            var vm = new WorkoutDetailViewModel(NavService.Object, fitnessService, LocalDB.Object);

            await vm.LoadExercises("1");

            vm.WorkoutExercises.Should().BeEquivalentTo(ExpectedExercises);

        }
    }
}