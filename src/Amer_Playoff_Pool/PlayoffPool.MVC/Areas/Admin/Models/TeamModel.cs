namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TeamModel
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string Abbreviation { get; set; }
    }
}
