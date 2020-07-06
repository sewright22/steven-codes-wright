using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinHelper.Core
{
    public class HashHelper : IHashHelper
    {
        public string Hash(string password, int iterations)
        {
            return SecurePasswordHasher.Hash(password, iterations);
        }

        public string Hash(string password)
        {
            return SecurePasswordHasher.Hash(password);
        }

        public bool IsHashSupported(string hashString)
        {
            return SecurePasswordHasher.IsHashSupported(hashString);
        }

        public bool Verify(string password, string hashedPassword)
        {
            return SecurePasswordHasher.Verify(password, hashedPassword);
        }
    }

    public interface IHashHelper
    {
        string Hash(string password, int iterations);
        string Hash(string password);
        bool IsHashSupported(string hashString);
        bool Verify(string password, string hashedPassword);
    }
}


