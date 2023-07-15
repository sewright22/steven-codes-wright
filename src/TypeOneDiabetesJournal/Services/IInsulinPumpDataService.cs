using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;

namespace Services
{
    public interface IInsulinPumpDataService
    {
        string Token { get; }
        Task Login(string username, string password);
        void UpdateToken(string token, string userId);
        Task<ReadingList> GetInsulinPumpDataAsync(DateTime startDate, DateTime endDate);
    }
}
