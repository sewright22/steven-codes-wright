namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Models;
    using System.ComponentModel;

    public class RoundModel : IModal
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int PlayoffId { get; set; }

        [DisplayName("Round Number")]
        public int Number { get; set; }

        [DisplayName("Points")]
        public int PointValue { get; set; }
        public required string Name { get; set; }
        public List<string>? Winners { get; set; }
        public List<SelectListItem> Teams { get; } = new ();
        public List<SelectListItem> Rounds { get; } = new ();
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
