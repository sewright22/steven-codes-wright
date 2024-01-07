namespace PlayoffPool.MVC.Extensions
{
    using AmerFamilyPlayoffs.Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using PlayoffPool.MVC.Areas.Admin.Models;

    /// <summary>
    /// Extension methods to help with managing playoff rounds.
    /// </summary>
    public static class RoundManager
    {
        public static void AddPlayoffRound(this AmerFamilyPlayoffContext dataContext, RoundModel model)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Name.HasValue() == false)
            {
                throw new ArgumentException("Round name is required.", nameof(model));
            }

            if (int.TryParse(model.Name, out int roundId) == false)
            {
                throw new ArgumentException("Round name must be a number.", nameof(model));
            }

            var round = new PlayoffRound()
            {
                RoundId = roundId,
                PointValue = model.PointValue,
                PlayoffId = model.PlayoffId,
            };

            dataContext.PlayoffRounds.Add(round);
            dataContext.SaveChanges();
        }

        public static RoundModel GetPlayoffRound(this AmerFamilyPlayoffContext dbContext, int id)
        {
            var playoffRound = dbContext.PlayoffRounds
                .AsNoTracking()
                .Include(x => x.Round)
                .Include(x => x.Playoff)
                .Include(x => x.RoundWinners)
                .FirstOrDefault(x => x.Id == id);

            if (playoffRound == null)
            {
                throw new KeyNotFoundException(nameof(id));
            }

            var roundModel = new RoundModel
            {
                Id = playoffRound.Id,
                Name = playoffRound.Round.Id.ToString(),
                PointValue = playoffRound.PointValue,
                Number = playoffRound.Round.Number,
                SeasonId = playoffRound.Playoff.SeasonId,
            };

            roundModel.Teams.AddRange(dbContext.GetTeamsForRound(playoffRound.Id).ToList());

            roundModel.Winners = playoffRound.RoundWinners?.Select(x => x.PlayoffTeamId.ToString()).ToList();

            roundModel.Rounds.AddRange(dbContext.GetRounds().ToList());

            return roundModel;
        }

        public static IEnumerable<SelectListItem> GetTeamsForRound(this AmerFamilyPlayoffContext dataContext, int playoffRoundId)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            var playoffId = dataContext.PlayoffRounds
                .FirstOrDefault(x => x.Id == playoffRoundId)?.PlayoffId;

            if (playoffId == null)
            {
                throw new KeyNotFoundException(nameof(playoffRoundId));
            }

            var teams = dataContext.PlayoffTeams
                .Include(x => x.SeasonTeam)
                .Where(x => x.PlayoffId == playoffId)
                .OrderBy(x => x.SeasonTeam.Team.Name)
                .Select(x => new SelectListItem
                {
                    Text = x.SeasonTeam.Team.Name,
                    Value = x.Id.ToString(),
                });

            return teams;
        }

        public static void UpdatePlayoffRound(this AmerFamilyPlayoffContext dataContext, RoundModel model)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Name.HasValue() == false)
            {
                throw new ArgumentException("Round name is required.", nameof(model));
            }

            if (int.TryParse(model.Name, out int roundId) == false)
            {
                throw new ArgumentException("Round name must be a number.", nameof(model));
            }

            var playoffRound = dataContext.PlayoffRounds
                .FirstOrDefault(x => x.Id == model.Id);

            if (playoffRound == null)
            {
                throw new KeyNotFoundException(nameof(model.Id));
            }

            playoffRound.RoundId = roundId;
            playoffRound.PointValue = model.PointValue;

            if (playoffRound.RoundWinners == null)
            {
                playoffRound.RoundWinners = new List<RoundWinner>();
            }

            playoffRound.RoundWinners.Clear();

            if (model.Winners != null)
            {
                foreach (var winner in model.Winners)
                {
                    var playoffTeam = dataContext.PlayoffTeams
                        .FirstOrDefault(x => x.Id == int.Parse(winner));

                    if (playoffTeam == null)
                    {
                        throw new KeyNotFoundException(nameof(winner));
                    }

                    playoffRound.RoundWinners.Add(new RoundWinner
                    {
                        PlayoffTeamId = playoffTeam.Id,
                    });
                }
            }

            dataContext.SaveChanges();
        }
    }
}
