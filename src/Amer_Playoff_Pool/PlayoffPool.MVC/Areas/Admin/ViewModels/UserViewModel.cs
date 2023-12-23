namespace PlayoffPool.MVC.Areas.Admin.ViewModels
{
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Models;
    using System;

    public class UserViewModel : IModal
    {
        public UserModel? UserModel { get; set; }

        public string? Title
        {
            get
            {
                return "Update User";
            }
            set
            {

            }
        }
    }
}
