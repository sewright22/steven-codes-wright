using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DiabetesFoodJournal.Services
{
    public class SecureStorageHelper : ISecureStorage
    {
        public Task<string> GetAsync(string key)
        {
            return SecureStorage.GetAsync(key);
        }

        public Task SetAsync(string key, string value)
        {
            return SecureStorage.SetAsync(key, value);
        }
    }

    public interface ISecureStorage
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}
