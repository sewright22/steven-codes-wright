namespace PlayoffPool.MVC.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PlayoffPool.MVC.Areas.Admin.Models;

    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return this.View(new UsersModel());
        }
    }
}
