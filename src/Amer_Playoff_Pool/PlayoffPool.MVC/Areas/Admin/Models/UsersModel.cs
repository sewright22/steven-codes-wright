namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using PlayoffPool.MVC.Models;

    public class UsersModel : IBreadcrumb
    {
        public List<UserModel> Users { get; } = new List<UserModel>();

        public List<BreadcrumbItemModel> BreadcrumbList => new List<BreadcrumbItemModel>
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
    }
}
