namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using PlayoffPool.MVC.Models;

    public class AdminViewModel : IBreadcrumb
    {
        public List<BreadcrumbItemModel> BreadcrumbList => new List<BreadcrumbItemModel>
        {
            new BreadcrumbItemModel
            {
                Text = "Admin",
                Url = "/Admin",
                IsActive = true,
            },
        };
    }
}
