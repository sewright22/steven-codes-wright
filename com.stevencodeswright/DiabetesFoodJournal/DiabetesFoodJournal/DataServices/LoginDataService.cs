using DiabetesFoodJournal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinHelper.Core;

namespace DiabetesFoodJournal.DataServices
{
    public class LoginDataService : ILoginDataService
    {
        private readonly IAppDataService appDataService;
        private readonly IUserInfo userInfo;
        private readonly IHashHelper hashHelper;

        public LoginDataService(IAppDataService appDataService, IUserInfo userInfo, IHashHelper hashHelper)
        {
            this.appDataService = appDataService;
            this.userInfo = userInfo;
            this.hashHelper = hashHelper;
        }
        public async Task CreateAccount(string email, string password)
        {
            var user = await this.appDataService.CreateAccount(email, password).ConfigureAwait(false);

            if (user != null)
            {
                await this.userInfo.SetUserId(user.Id);
                await this.userInfo.SetUserEmail(user.Email);
                await this.userInfo.SetUserPassword(password);
            }
            else
            {
                throw new Exception();
            }
        }

        public Task<string> GetPassword()
        {
            return this.userInfo.GetUserPassword();
        }

        public Task<string> GetUserName()
        {
            return this.userInfo.GetUserEmail();
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
        Task<string> GetUserName();
        Task<string> GetPassword();
    }
}
