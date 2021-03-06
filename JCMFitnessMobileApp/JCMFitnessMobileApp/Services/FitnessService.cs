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
            try
            {
                return await _fitApi.GetUsersAsync(azureCode);
            }
            catch(Exception ex)
            {
                throw;

            }
        }
        public async Task<User> GetUserById(string id)
        {
            try
            {
                return await _fitApi.GetUserByIdAsync(id, azureCode);
            }
            catch (Exception ex)
            {
                throw;

            }
        }

    }

    public interface IFitApi
    {
        [Get("api/users?code={azureCode}")]
        public Task<List<User>> GetUsersAsync(string azureCode);

        [Get("api/users/id={userid}?code={azureCode}")]
        public Task<User> GetUserByIdAsync(string userid,string azureCode);
    }
}
