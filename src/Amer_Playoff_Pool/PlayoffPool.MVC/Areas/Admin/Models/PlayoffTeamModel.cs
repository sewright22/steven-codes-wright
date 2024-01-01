namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Models;
    using System.ComponentModel;

    public class PlayoffTeamModel : IModal
    {
        public int Id { get; set; }
        public int PlayoffId { get; set; }
        [DisplayName("Team")]
        public int? TeamId { get; set; }
        public string? Name { get; set; }
        public int PlayoffTeamId { get; set; }
        public int Seed { get; set; }

        [DisplayName("Conference")]
        public int ConferenceId { get; set; }
        public int SeasonId { get; set; }
        public List<SelectListItem> Conferences { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Teams { get; set; } = new ();
        public string? Title
        {
            get
            {
                return this.Name.HasValue() ? $"{this.Name}" : "Add Playoff Team";
            }
            set
            {

            }
        }
    }
}
