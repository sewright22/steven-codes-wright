namespace PlayoffPool.MVC.Models
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {
            this.BreadcrumbList.Add(new BreadcrumbItemModel()
            {
                Text = "Admin",
                Url = "/Admin",
            });

            this.SetActiveBreadcrumbs();
        }

        // Create container for the breadcrumb
        public List<BreadcrumbItemModel> BreadcrumbList { get; } = new List<BreadcrumbItemModel>();

        public void SetActiveBreadcrumbs()
        {
            for (int currentIndex = 0; currentIndex < this.BreadcrumbList.Count; currentIndex++)
            {
                this.BreadcrumbList[currentIndex].IsActive = currentIndex == this.BreadcrumbList.Count - 1;
            }
        }
    }
}
