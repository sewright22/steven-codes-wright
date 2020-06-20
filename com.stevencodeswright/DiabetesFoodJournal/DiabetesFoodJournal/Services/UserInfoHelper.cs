using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Services
{
    public class UserInfoHelper : IUserInfo
    {
        private readonly ISecureStorage secureStorage;

        public UserInfoHelper(ISecureStorage secureStorage)
        {
            this.secureStorage = secureStorage;
        }

        public Task<string> GetDexcomRefreshToken()
        {
            return this.secureStorage.GetAsync("refresh");
        }

        public Task<string> GetDexcomToken()
        {
            return this.secureStorage.GetAsync("token");
        }

        public Task<string> GetDexcomTokenType()
        {
            return this.secureStorage.GetAsync("tokenType");
        }

        public Task<string> GetUserEmail()
        {
            return this.secureStorage.GetAsync("userEmail");
        }

        public async Task<int> GetUserId()
        {
            return Convert.ToInt32(await this.secureStorage.GetAsync("userId").ConfigureAwait(false));
        }

        public Task<string> GetUserPassword()
        {
            return this.secureStorage.GetAsync("userPassword");
        }

        public Task SetDexcomRefreshToken(string refreshToken)
        {
            return this.secureStorage.SetAsync("refresh", refreshToken);
        }

        public Task SetDexcomToken(string token)
        {
            return this.secureStorage.SetAsync("token", token);
        }

        public Task SetDexcomTokenType(string tokenType)
        {
            return this.secureStorage.SetAsync("tokenType", tokenType);
        }

        public Task SetUserEmail(string userEmail)
        {
            return this.secureStorage.SetAsync("userEmail", userEmail);
        }

        public Task SetUserId(int userId)
        {
            return this.secureStorage.SetAsync("userId", userId.ToString()); ;
        }

        public Task SetUserPassword(string password)
        {
            return this.secureStorage.SetAsync("userPassword", password);
        }
    }

    public interface IUserInfo
    {
        Task<int> GetUserId();
        Task<string> GetUserEmail();
        Task<string> GetUserPassword();
        Task<string> GetDexcomToken();
        Task<string> GetDexcomRefreshToken();
        Task SetDexcomToken(string token);
        Task SetDexcomRefreshToken(string refreshToken);
        Task SetUserId(int userId);
        Task SetUserEmail(string userEmail);
        Task SetUserPassword(string password);
    }
}
