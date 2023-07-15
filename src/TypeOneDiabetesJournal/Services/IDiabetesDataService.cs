using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Responses;

namespace Services
{
    public interface IDiabetesDataService
    {
        public Task<LoginResponse> Login(string username, string password);
        public Task<string> GetFitbitLink(string token);

        public Task<FoodLogResponse> GetFoodLog(string token, DateTime date);
    }
}
