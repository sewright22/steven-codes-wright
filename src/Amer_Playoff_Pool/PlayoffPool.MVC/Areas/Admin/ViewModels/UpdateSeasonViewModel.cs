namespace PlayoffPool.MVC.Areas.Admin.ViewModels
{
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Models;
    using System.Collections.Generic;

    public class UpdateSeasonViewModel : IBreadcrumb
    {
        public required SeasonModel Season { get; set; }

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
                Text = "Manage Seasons",
                Url = "/Admin/Season",
                IsActive = true,
            },
        };

        internal void AddBreadcrumb(string displayText)
        {
            var lastItem = this.BreadcrumbList.LastOrDefault();

            if (lastItem != null)
            {
                lastItem.IsActive = false;
            }

            this.BreadcrumbList.Add(new BreadcrumbItemModel
            {
                Text = displayText.HasValue(",") ? displayText : "New Season",
                Url = "/Admin/Season/Index",
                IsActive = true,
            });
        }
    }
}
