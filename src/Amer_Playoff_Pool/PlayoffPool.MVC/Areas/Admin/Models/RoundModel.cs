namespace PlayoffPool.MVC.Areas.Admin.Models
{
    public class RoundModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int PointValue { get; set; }
        public required string Name { get; set; }
        public List<string>? SelectedTeams { get; set; }
        public List<TeamModel> Teams { get; } = new List<TeamModel>();
    }
}
