namespace PlayoffPool.MVC.Extensions
{
    using AmerFamilyPlayoffs.Data;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Models;
    using PlayoffPool.MVC.Models.Home;

    public static class BracketPredictionManager
    {
        public static IQueryable<PlayoffRound> GetCurrentSeasonPlayoffRounds(this AmerFamilyPlayoffContext dbContext)
        {
            return dbContext.PlayoffRounds.AsNoTracking()
                .Include(x => x.Round)
                .Include(x => x.Playoff)
                .Where(x => x.Playoff.SeasonId == dbContext.GetCurrentSeasonId());
        }

        public static int GetCurrentSeasonId(this AmerFamilyPlayoffContext dbContext)
        {
            return dbContext.Seasons.AsNoTracking()
                .OrderBy(x => x.Year)
                .Select(x => x.Id)
                .LastOrDefault();
        }

        public static IQueryable<BracketPrediction> ForUserId(this IQueryable<BracketPrediction> bracketPredictions, string? userId)
        {
            return bracketPredictions.Where(x => x.UserId == userId);
        }

        public static IQueryable<BracketPrediction> GetCompletedBrackets(this IQueryable<BracketPrediction> bracketPredictions)
        {
            return bracketPredictions
            .Where(x => x.MatchupPredictions.Count(x => x.PredictedWinner != null) == 13);
        }

        public static IQueryable<BracketPrediction> GetIncompleteBrackets(this IQueryable<BracketPrediction> bracketPredictions)
        {
            return bracketPredictions
            .Where(x => x.MatchupPredictions.Count(x => x.PredictedWinner != null) < 13);
        }

        public static IQueryable<BracketSummaryModel> AsBracketSummaryModel(this IQueryable<BracketPrediction> bracketPredictions)
        {
            return bracketPredictions
                .Select(
                x => new BracketSummaryModel()
                {
                    Id = x.Id,
                    PredictedWinner = x.MatchupPredictions.Any(x => x.PlayoffRound.Round.Number == 4) == false
                    ? null
                    : x.MatchupPredictions.FirstOrDefault(mp => mp.PlayoffRound.Round.Number == 4)!.PredictedWinner.ToPlayoffTeamViewModel(),
                });
        }

        public static BracketModel? GetBracket(this AmerFamilyPlayoffContext dbContext, int bracketId)
        {
            return dbContext.BracketPredictions
                .AsNoTracking()
                .AsBracketModel()
                .FirstOrDefault(x => x.Id == bracketId);
        }

        public static IQueryable<BracketModel> AsBracketModel(this IQueryable<BracketPrediction> bracketPredictions)
        {
            return bracketPredictions
                .Include(x => x.Playoff)
                .Select(
                    x => new BracketModel()
                    {
                        Id = x.Id,
                        SeasonId = x.Playoff.SeasonId,
                        Name = x.Name,
                    });
        }

        public static IQueryable<BracketModel> GetBracketsForYear(this AmerFamilyPlayoffContext dbContext, int seasonId)
        {
            return dbContext.BracketPredictions
                .AsNoTracking()
                .Where(x => x.Playoff.Season.Id == seasonId)
                .AsBracketModel();
        }

        public static void UpdateBracket(this AmerFamilyPlayoffContext dbContext, BracketModel bracketModel)
        {
            var bracket = dbContext.BracketPredictions
                .Include(x => x.MatchupPredictions)
                .FirstOrDefault(x => x.Id == bracketModel.Id);

            if (bracket is null)
            {
                return;
            }

            bracket.Name = bracketModel.Name;

            dbContext.SaveChanges();
        }

        public static void DeleteBracket(this AmerFamilyPlayoffContext dbContext, int bracketId)
        {
            var bracket = dbContext.BracketPredictions
                .FirstOrDefault(x => x.Id == bracketId);

            if (bracket is null)
            {
                return;
            }

            dbContext.BracketPredictions.Remove(bracket);

            dbContext.SaveChanges();
        }

        public static PlayoffTeamViewModel ToPlayoffTeamViewModel(this PlayoffTeam playoffTeam)
        {
            return new PlayoffTeamViewModel()
            {
                Id = playoffTeam.Id,
                Name = playoffTeam.SeasonTeam.Team.Name,
                Seed = playoffTeam.Seed,
            };
        }
    }
}
