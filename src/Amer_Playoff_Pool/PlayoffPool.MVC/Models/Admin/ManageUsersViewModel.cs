namespace PlayoffPool.MVC.Models
{
	public class ManageUsersViewModel : AdminViewModel
	{
		public ManageUsersViewModel()
		{
            this.BreadcrumbList.Add(new BreadcrumbItemModel()
			{
                Text = "Manage Users",
                Url = "/Admin/Users",
            });

			this.SetActiveBreadcrumbs();
        }
		public List<UserModel> Users { get; } = new List<UserModel>();
	}
}
