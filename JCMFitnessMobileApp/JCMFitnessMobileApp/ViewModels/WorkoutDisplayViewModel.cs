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
        public User CurrentUser { get; set; }
        public User admin { get; set; }
        public List<Workout> WorkoutList { get; set; }

        public WorkoutDisplayViewModel(IFitnessService newsService)
        {
            this.fitnessService = newsService;
        }
        public async Task Init()
        {

            try
            {
                CurrentUser = await fitnessService.GetUserById("2");


                admin = await fitnessService.LoginUser("max", "123");
            }
            catch
            {
                throw;
            }



        }
    }
}
