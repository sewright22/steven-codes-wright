using AmerFamilyPlayoffs.Data;
using Elfie.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayoffPool.MVC.Areas.Admin.Models;
using System.Linq.Expressions;

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

        public static IQueryable<TeamModel> GetTeams(this AmerFamilyPlayoffContext dbContext)
        {
            return dbContext.Teams.AsNoTracking()
            .Select(
                x => new TeamModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Abbreviation = x.Abbreviation,
                });
        }

        public static IQueryable<SeasonSummaryModel> GetSeasons(this AmerFamilyPlayoffContext dbContext)
        {
            return dbContext.Seasons.AsNoTracking()
            .Select(
                x => new SeasonSummaryModel
                {
                    Id = x.Id,
                    Name = x.Year.ToString(),
                }).OrderByDescending(x => x.Name);
        }

        public static SeasonModel GetSeason(this AmerFamilyPlayoffContext dbContext, int id)
        {
            var season = dbContext.Seasons.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (season == null)
            {
                throw new KeyNotFoundException(nameof(id));
            }

            var seasonModel = new SeasonModel
            {
                Id = season.Id,
                Year = season.Year.ToString(),
            };

            seasonModel.Rounds.AddRange(dbContext.GetRounds().ToList());

            return seasonModel;
        }

        public static IQueryable<RoundModel> GetRounds(this AmerFamilyPlayoffContext dbContext)
        {
            return dbContext.Rounds.AsNoTracking()
            .Select(
                x => new RoundModel
                {
                    Id = x.Id,
                    Name = x.Name,
                });
        }

        public static UserModel GetUser(this AmerFamilyPlayoffContext dbContext, string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return new UserModel();
            }

            var user = dbContext.Users.AsNoTracking()
            .Select(
                x => new UserModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                })
            .FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException(nameof(id));
            }

            var roleForUser = dbContext.UserRoles.AsNoTracking().FirstOrDefault(x => x.UserId == id);
            var roles = dbContext.Roles.AsNoTracking().ToList();

            if (roleForUser != null)
            {
                user.RoleId = roleForUser.RoleId;
                user.Role = roles.FirstOrDefault(x => x.Id == roleForUser.RoleId)?.Name;
            }

            user.Roles.AddRange(roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id,
            }));

            return user;
        }

        public static async Task UpdateUser(this AmerFamilyPlayoffContext dbContext, UserModel userModel)
        {
            var userToUpdate = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userModel.Id);

            userToUpdate.Update(userModel);

            await dbContext.SaveChangesAsync();
        }

        public static async Task CreateSeason(this AmerFamilyPlayoffContext dbContext, SeasonModel seasonModel)
        {
            var season = new Season();

            season.Update(seasonModel);

            await dbContext.Seasons.AddAsync(season);

            await dbContext.SaveChangesAsync();
        }

        public static async Task CreateUser(this AmerFamilyPlayoffContext dbContext, UserModel userModel)
        {
            var user = new User();

            user.Update(userModel);

            await dbContext.Users.AddAsync(user);

            await dbContext.SaveChangesAsync();
        }

        public static async Task UpdateRoleForUser(this UserManager<User> userManager, string? userId, string? roleId, RoleManager<IdentityRole> roleManager)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(roleId))
            {
                return;
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return;
            }

            var userRoles = await userManager.GetRolesAsync(user);

            if (userRoles.Any())
            {
                await userManager.RemoveFromRolesAsync(user, userRoles);
            }

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null || string.IsNullOrEmpty(role.Name))
            {
                return;
            }

            await userManager.AddToRoleAsync(user, role.Name);
        }

        public static async Task UpdateRoleForUser(this AmerFamilyPlayoffContext dbContext, string? userId, string? roleId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(roleId))
            {
                return;
            }

            var userRole = dbContext.UserRoles.FirstOrDefault(x => x.UserId == userId);

            if (userRole == null)
            {
                return;
            }

            userRole.RoleId = roleId;
            await dbContext.SaveChangesAsync();
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

        public static void Update(this Season? seasonToUpdate, SeasonModel seasonModel)
        {
            if (seasonToUpdate == null)
            {
                return;
            }

            seasonToUpdate.Year = int.Parse(seasonModel.Year);
            seasonToUpdate.Description = seasonModel.Description;
        }
    }
}
