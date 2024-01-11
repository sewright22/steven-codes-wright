namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using Microsoft.EntityFrameworkCore.Update.Internal;
    using PlayoffPool.MVC.Models;

    public class BracketModel : IModal
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public required string Name { get; set; }
        public string? Title
        {
            get
            {
                return "Update Bracket";
            }
            set
            {

            }
        }
    }
}
