namespace PlayoffPool.MVC.Extensions
{
    using AmerFamilyPlayoffs.Data;
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

            roundModel.Rounds.AddRange(dbContext.GetRounds().ToList());

            return roundModel;
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

            dataContext.SaveChanges();
        }
    }
}
