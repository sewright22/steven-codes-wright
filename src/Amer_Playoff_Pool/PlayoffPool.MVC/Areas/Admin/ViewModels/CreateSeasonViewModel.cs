namespace PlayoffPool.MVC.Areas.Admin.ViewModels
{
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Models;

    public class CreateSeasonViewModel : IModal
    {
        public string? Title
        {
            get
            {
                return "Create Season";
            }
            set
            { }
        }

        public required SeasonModel Season { get; set; }
    }
}
