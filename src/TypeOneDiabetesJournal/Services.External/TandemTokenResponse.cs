using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;

namespace Services.External
{
    [Serializable]
    public class TandemTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTimeOffset AccessTokenExpiresAt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTimeOffset RefreshTokenExpiresAt { get; set; }
        public string Scope { get; set; } = string.Empty;
        public TandemClient? Client { get; set; }
        public TandemUser? User { get; set; }
    }
}
