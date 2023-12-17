namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TeamModel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Abbreviation { get; set; }
    }
}
