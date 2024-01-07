namespace PlayoffPool.MVC.Models.Home
{
    public class LeaderboardViewModel
    {
        public bool ShowLeaderboard { get; set; }
        public List<BracketSummaryModel> Brackets { get; set; } = new List<BracketSummaryModel>();
    }
}
