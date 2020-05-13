namespace FJA_Pool.Data
{
    using FJA_Pool.Entities;
    using MySql.Data.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class FjaPoolContext : DbContext
    {
        // Your context has been configured to use a 'FjaPoolEntity' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FJA_Pool.Data.FjaPoolEntity' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FjaPoolEntity' 
        // connection string in the application configuration file.
        public FjaPoolContext()
            : base("name=FjaPoolEntity")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TeamEntity>();
            modelBuilder.Entity<BracketPickEntity>();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<BracketPickEntity> BracketPicks { get; set; }
    }
}