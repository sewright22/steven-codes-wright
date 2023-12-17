namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using PlayoffPool.MVC.Models;

    public class SeasonsModel : IBreadcrumb
    {
        public List<SeasonModel> Seasons { get; } = new List<SeasonModel>();

        public List<BreadcrumbItemModel> BreadcrumbList => new List<BreadcrumbItemModel>
        {
            new BreadcrumbItemModel
            {
                Text = "Admin",
                Url = "/Admin",
                IsActive = false,
            },
            new BreadcrumbItemModel
            {
                Text = "Seasons",
                IsActive = true,
            },
        };
    }
}
