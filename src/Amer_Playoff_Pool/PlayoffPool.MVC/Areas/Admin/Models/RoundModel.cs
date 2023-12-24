namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Models;

    public class RoundModel : IModal
    {
        public int Id { get; set; }
        public int PlayoffId { get; set; }
        public int Number { get; set; }
        public int PointValue { get; set; }
        public required string Name { get; set; }
        public List<string>? SelectedTeams { get; set; }
        public List<TeamModel> Teams { get; } = new List<TeamModel>();
        public string? Title
        {
            get
            {
                return this.Name.HasValue() ? $"Round {this.Number} - {this.Name}" : "Add Round";
            }
            set
            {

            }
        }
    }
}
