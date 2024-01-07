namespace PlayoffPool.MVC.Extensions
{
    using AmerFamilyPlayoffs.Data;

    public static class PlayoffManager
    {
        public static void UpdatePlayoffStartDateTime(this AmerFamilyPlayoffContext dataContext, int playoffId, DateTime? cutoffDateTime)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            if (cutoffDateTime.HasValue == false)
            {
                return;
            }

            Playoff? playoff = dataContext.Playoffs.FirstOrDefault(p => p.Id == playoffId);

            if (playoff is null)
            {
                return;
            }

            playoff.StartDateTime = cutoffDateTime.Value.ToUniversalTime();

            dataContext.SaveChanges();
        }

        public static int GetCurrentPlayoffId(this AmerFamilyPlayoffContext dataContext)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            int seasonId = dataContext.GetCurrentSeasonId();

            Playoff? playoff = dataContext.Playoffs.FirstOrDefault(p => p.SeasonId == seasonId);

            if (playoff is null)
            {
                throw new Exception("No playoff found for current season.");
            }

            return playoff.Id;
        }

        public static bool IsPlayoffStarted(this AmerFamilyPlayoffContext dataContext)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            int playoffId = dataContext.GetCurrentPlayoffId();

            return dataContext.IsPlayoffStarted(playoffId);
        }

        public static bool IsPlayoffStarted(this AmerFamilyPlayoffContext dataContext, int playoffId)
        {
            if (dataContext is null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            Playoff? playoff = dataContext.Playoffs.FirstOrDefault(p => p.Id == playoffId);

            if (playoff is null)
            {
                return false;
            }

            return playoff.StartDateTime.HasValue && playoff.StartDateTime.Value < DateTime.UtcNow;
        }
    }
}
