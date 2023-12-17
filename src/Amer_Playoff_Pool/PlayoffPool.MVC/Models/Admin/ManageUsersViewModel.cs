﻿namespace PlayoffPool.MVC.Models
{
	public class ManageUsersViewModel : IBreadcrumb
	{
		public List<UserModel> Users { get; } = new List<UserModel>();

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
                Text = "Manage Users",
                Url = "/Admin/ManageUsers",
                IsActive = true,
            },
        };
    }
}
