namespace PlayoffPool.MVC.Areas.Admin.Models
{
    using PlayoffPool.MVC.Models;

    public class BracketsModel : IBreadcrumb
    {
        public BracketsModel()
        {
            this.Brackets = new List<BracketModel>();
        }

        public List<BracketModel> Brackets { get; }

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
