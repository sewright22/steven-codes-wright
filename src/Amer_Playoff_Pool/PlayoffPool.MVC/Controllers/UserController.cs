namespace PlayoffPool.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PlayoffPool.MVC.Helpers;

    public class UserController : Controller
    {
        public UserController(ILogger<UserController> logger, IDataManager dataManager)
        {
            this.Logger = logger;
        }

        public ILogger<UserController> Logger { get; }

        public IActionResult Index()
        {
            return View();
        }
    }
}
