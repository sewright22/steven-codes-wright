namespace PlayoffPool.MVC.Areas.Admin.Models
{
    public class SeasonModel
    {
        public required int Id { get; set; }
        public required string Year { get; set; }
        public string? Description { get; set; }
        public int? PlayoffId { get; set; }
        public List<RoundModel> Rounds { get; } = new ();
        public List<PlayoffTeamModel> Teams { get; } = new ();
    }
}
