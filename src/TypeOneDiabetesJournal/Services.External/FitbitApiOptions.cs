using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.External
{
    public class FitbitApiOptions
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? RedirectUrl { get; set; }
        public string? Scope { get; set; }
    }
}
