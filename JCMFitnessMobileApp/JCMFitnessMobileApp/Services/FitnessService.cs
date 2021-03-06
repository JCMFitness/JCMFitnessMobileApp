using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;
using Refit;

namespace JCMFitnessMobileApp.Services
{

    public class FitnessService : IFitnessService
    {
        private readonly IFitApi _fitApi;
        private string azureCode;

        public FitnessService(IFitApi fitApi)
        {
            this._fitApi = fitApi;
            azureCode = Settings.apiKey;
        }

        public async Task<List<User>> GetUsers()
        {
            
        }
    }

    public interface IFitApi
    {
        [Get("/users?code={azureCode}")]
        public Task<List<User>> GetUsersAsync(string azureCode);
    }
}
