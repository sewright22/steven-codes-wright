namespace PlayoffPool.MVC.Models.Admin
{
    using System.Collections.Generic;

    public class SeasonListViewModel : IBreadcrumb
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
                IsActive = true,
            },
        };
    }
}
