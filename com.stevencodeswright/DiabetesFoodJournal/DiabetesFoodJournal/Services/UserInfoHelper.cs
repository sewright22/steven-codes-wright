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
    }

    public interface IUserInfo
    {
        Task<string> GetDexcomToken();
        Task<string> GetDexcomRefreshToken();
        Task SetDexcomToken(string token);
        Task SetDexcomRefreshToken(string refreshToken);
    }
}
