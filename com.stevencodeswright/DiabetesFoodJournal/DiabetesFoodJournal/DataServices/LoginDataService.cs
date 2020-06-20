using DiabetesFoodJournal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.DataServices
{
    public class LoginDataService : ILoginDataService
    {
        private readonly IAppDataService appDataService;
        private readonly IUserInfo userInfo;

        public LoginDataService(IAppDataService appDataService, IUserInfo userInfo)
        {
            this.appDataService = appDataService;
            this.userInfo = userInfo;
        }
        public Task CreateAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await this.appDataService.Login(email, password).ConfigureAwait(false);

            if (user != null)
            {
                await this.userInfo.SetUserId(user.Id);
                await this.userInfo.SetUserEmail(user.Email);
                await this.userInfo.SetUserPassword(password);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public interface ILoginDataService
    {
        Task<bool> Login(string email, string password);
        Task CreateAccount(string email, string password);
    }
}
