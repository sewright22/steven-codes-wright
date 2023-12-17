namespace PlayoffPool.MVC.Areas.Admin.ViewModels
{
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Models;

    public class CreateSeasonViewModel : IBreadcrumb
    {
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
                Url = "/Admin/Seasons",
                IsActive = false,
            },
            new BreadcrumbItemModel
            {
                Text = "Create Season",
                IsActive = true,
            },
        };

        public required SeasonModel Season { get; set; }
    }
}
