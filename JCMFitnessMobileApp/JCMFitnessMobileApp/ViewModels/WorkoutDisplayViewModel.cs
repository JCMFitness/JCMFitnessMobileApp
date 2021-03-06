using JCMFitnessMobileApp.Models;
using JCMFitnessMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCMFitnessMobileApp.ViewModels
{
    public class WorkoutDisplayViewModel : ViewModel
    {
        private readonly IFitnessService fitnessService;
        public List<User> CurrentUsers { get; set; }
        public User admin { get; set; }
        public List<Workout> WorkoutList { get; set; }

        public WorkoutDisplayViewModel(IFitnessService newsService)
        {
            this.fitnessService = newsService;
        }
        public async Task Init()
        {
            CurrentUsers = await fitnessService.GetUsers();
            admin = CurrentUsers.First();
            admin = await fitnessService.GetUserById(admin.Id);
            WorkoutList = admin.Workouts;
        }
    }
}
