using FJA_Pool.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FJA_Pool.Data
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<FjaPoolContext>
    {
        protected override void Seed(FjaPoolContext context)
        {
            base.Seed(context);

            //var team1 = new TeamEntity()
            //{
            //    City = "City",
            //    Name = "Team Name",
            //    Conference = "AFC",
            //    Losses = 1,
            //    Wins = 3,
            //    Season = "2017",
            //    Seed = 1
            //};

            //context.Teams.Add(team1);

            context.SaveChanges();
        }
    }
}
