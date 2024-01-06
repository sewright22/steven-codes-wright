namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using PlayoffPool.MVC.Models;

    public class SeasonModel : IModal
    {
        public required int Id { get; set; }
        public required string Year { get; set; }
        public string? Description { get; set; }
        public int? PlayoffId { get; set; }
        public DateTime? CutoffDateTime { get; set; }
        public List<RoundModel> Rounds { get; } = new();
        public List<PlayoffTeamModel> Teams { get; } = new();
        public string? Title
        {
            get
            {
                return $"{this.Year} Season";
            }
            set { }
        }
    }
}
