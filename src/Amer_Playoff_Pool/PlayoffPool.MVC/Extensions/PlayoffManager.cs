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

            playoff = cutoffDateTime.Value;

            dataContext.SaveChanges();
        }
    }
}
