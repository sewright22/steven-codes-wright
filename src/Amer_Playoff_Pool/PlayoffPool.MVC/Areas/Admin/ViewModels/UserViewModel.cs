namespace PlayoffPool.MVC.Areas.Admin.ViewModels
{
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Models;
    using System;

    public class UserViewModel : IBreadcrumb
    {
        public UserModel? UserModel { get; set; }

        public List<BreadcrumbItemModel> BreadcrumbList { get; } = new()
        {
            new BreadcrumbItemModel
            {
                Text = "Admin",
                Url = "/Admin/Home",
                IsActive = false,
            },
            new BreadcrumbItemModel
            {
                Text = "Users",
                Url = "/Admin/User/Index",
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
                Text = displayText.HasValue(",") ? displayText : "New User",
                Url = "/Admin/User/Index",
                IsActive = true,
            });
        }
    }
}
