namespace PlayoffPool.MVC.Areas.Admin.Models
{
    public class SeasonModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Winner { get; set; }
    }
}
