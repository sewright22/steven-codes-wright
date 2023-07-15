using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace Migrations
{
    public class MigrationDbContext : sewright22_foodjournalContext
    {
        public MigrationDbContext()
        {
        }

        public MigrationDbContext(DbContextOptions<sewright22_foodjournalContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: Figure out how to access connection string from user secrets.
                optionsBuilder.UseMySql("server=localhost;port=3306;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.6.44-mysql"));
            }
        }
    }
}
