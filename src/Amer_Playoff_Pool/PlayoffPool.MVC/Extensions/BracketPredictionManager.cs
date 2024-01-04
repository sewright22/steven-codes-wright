namespace PlayoffPool.MVC.Extensions
{
    using AmerFamilyPlayoffs.Data;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using PlayoffPool.MVC.Models;
    using PlayoffPool.MVC.Models.Home;

    public static class BracketPredictionManager
    {
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
