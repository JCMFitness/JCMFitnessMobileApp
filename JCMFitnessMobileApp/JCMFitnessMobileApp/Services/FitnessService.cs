using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCMFitnessMobileApp.Models;


namespace JCMFitnessMobileApp.Services
{

    public class FitnessService : IFitnessService
    {
        private readonly IFitApi fitApi;


        public FitnessService(IFitApi newsApi)
        {
            this.fitApi = newsApi;
        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                return await fitApi.GetUserByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> LoginUser(string id, string password)
        {
            try
            {
                return await fitApi.UserLoginAsync(id, password);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }

}


