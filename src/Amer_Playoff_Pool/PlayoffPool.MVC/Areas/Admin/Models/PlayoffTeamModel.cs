namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PlayoffTeamModel : TeamModel
    {
        public int PlayoffTeamId { get; set; }
        public int Seed { get; set; }
        public int ConferenceId { get; set; }
        public List<SelectListItem> Conferences { get; set; } = new List<SelectListItem>();
    }
}
