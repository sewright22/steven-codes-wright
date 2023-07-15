using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeOneFoodJournal.Data;

namespace TypeOneFoodJournal.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:FoodJournal"];
            services.AddDbContext<FoodJournalContext>(o => o.UseLazyLoadingProxies().UseMySql(connectionString));
        }
    }
}
