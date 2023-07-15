using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.External
{
    internal static class Endpoints
    {
        internal static class Login
        {
            internal const string Path = "cloud/oauth2/token";
            internal const string GrantType = "grant_type";
            internal const string Username = "username";
            internal const string Password = "password";
            internal const string Scope = "scope";
        }

        internal static class PumpEvents
        {
            internal const string Path = "tconnect/therapyevents/api/TherapyEvents";
            internal const string UserId = "userId";
        }
    }
}
