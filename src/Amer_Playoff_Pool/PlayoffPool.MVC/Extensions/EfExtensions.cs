using AmerFamilyPlayoffs.Data;
using Microsoft.EntityFrameworkCore;
using PlayoffPool.MVC.Areas.Admin.Models;

namespace PlayoffPool.MVC.Extensions
{
    public static class EfExtensions
    {
        public static IQueryable<PlayoffTeam> FilterConference(this IQueryable<PlayoffTeam> playoffTeams, string conference)
        {
            return playoffTeams.Where(x => x.SeasonTeam.Conference.Name == conference);
        }

        public static PlayoffTeam GetTeamFromSeed(this IQueryable<PlayoffTeam> conferenceTeams, int seed)
        {
            var playoffTeam = conferenceTeams.FirstOrDefault(x => x.Seed == seed);

            if (playoffTeam == null)
            {
                throw new KeyNotFoundException(nameof(seed));
            }

            return playoffTeam;
        }

        public static IQueryable<UserModel> GetUsers(this AmerFamilyPlayoffContext dbContext)
        {
            return dbContext.Users.AsNoTracking()
            .Select(
                x => new UserModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                });
        }

        public static void Update(this User? userToUpdate, UserModel userModel)
        {
            if (userToUpdate == null)
            {
                return;
            }

            userToUpdate.FirstName = userModel.FirstName;
            userToUpdate.LastName = userModel.LastName;
            userToUpdate.Email = userModel.Email;
            userToUpdate.UserName = userModel.Email;
        }
    }
}
