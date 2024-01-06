namespace PlayoffPool.MVC.Extensions
{
    using AmerFamilyPlayoffs.Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using PlayoffPool.MVC.Areas.Admin.Models;

    public static class PlayoffTeamManager
    {
        public static IQueryable<PlayoffTeamModel> GetPlayoffTeams(this AmerFamilyPlayoffContext dataContext, int seasonId)
        {
            return dataContext.PlayoffTeams
                .Where(x => x.SeasonTeam.SeasonId == seasonId)
                .Select(x => new PlayoffTeamModel
                {
                    PlayoffTeamId = x.Id,
                    Name = x.SeasonTeam.Team.Name,
                    Seed = x.Seed,
                    SeasonId = x.SeasonTeam.SeasonId,
                    TeamId = x.SeasonTeam.TeamId,
                    ConferenceId = x.SeasonTeam.ConferenceId,
                    Conferences = dataContext.Conferences.AsNoTracking()
                    .Select(
                        y => new SelectListItem
                        {
                            Value = y.Id.ToString(),
                            Text = y.Name,
                        }).ToList(),
                });
        }

        //public static IQueryable<PlayoffTeamModel> GetPlayoffTeams(this AmerFamilyPlayoffContext dbContext, int seasonId)
        //{
        //    return dbContext.PlayoffTeams.AsNoTracking()
        //        .Where(x => x.Playoff.SeasonId == seasonId)
        //    .Select(
        //        x => new PlayoffTeamModel
        //        {
        //            Id = x.Id,
        //            Name = x.SeasonTeam.Team.Name,
        //            Abbreviation = x.SeasonTeam.Team.Abbreviation,
        //            Seed = x.Seed,
        //            ConferenceId = x.SeasonTeam.ConferenceId,
        //            Conferences = dbContext.Conferences.AsNoTracking()
        //            .Select(
        //                y => new SelectListItem
        //                {
        //                    Value = y.Id.ToString(),
        //                    Text = y.Name,
        //                }).ToList(),
        //        });
        //}

        public static void AddPlayoffTeam(this AmerFamilyPlayoffContext dataContext, PlayoffTeamModel model)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.TeamId.HasValue == false)
            {
                throw new ArgumentException("Team is required.", nameof(model));
            }


            // Find Team
            var team = dataContext.Teams.FirstOrDefault(x => x.Id == model.TeamId);

            if (team == null)
            {
                throw new KeyNotFoundException(nameof(model.Id));
            }

            // Find SeasonTeam
            var seasonTeam = dataContext.SeasonTeams.FirstOrDefault(x => x.TeamId == model.TeamId && x.SeasonId == model.SeasonId);

            if (seasonTeam == null)
            {
                seasonTeam = new SeasonTeam()
                {
                    TeamId = team.Id,
                    SeasonId = model.SeasonId,
                    ConferenceId = model.ConferenceId,
                };
            }

            dataContext.SeasonTeams.Add(seasonTeam);
            dataContext.SaveChanges();

            // Find PlayoffTeam
            var playoffTeam = dataContext.PlayoffTeams.FirstOrDefault(x => x.SeasonTeamId == seasonTeam.Id);

            if (playoffTeam != null)
            {
                return;
            }

            playoffTeam = new PlayoffTeam()
            {
                PlayoffId = model.PlayoffId,
                SeasonTeamId = seasonTeam.Id,
                Seed = model.Seed,
            };

            dataContext.PlayoffTeams.Add(playoffTeam);
            dataContext.SaveChanges();
        }

        public static IQueryable<SelectListItem> AsSelectListItems(this IQueryable<TeamModel> teams)
        {
            return teams.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
        }
    }
}
