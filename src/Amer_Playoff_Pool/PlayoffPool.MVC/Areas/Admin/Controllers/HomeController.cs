namespace PlayoffPool.MVC.Areas.Admin.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Controllers;
    using PlayoffPool.MVC.Helpers;

    [Area("Admin")]
    public class HomeController : Controller
    {
        public HomeController(ILogger<AdminController> logger, IDataManager dataManager)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.Logger = logger;
            this.DataManager = dataManager;
        }

        public ILogger<AdminController> Logger { get; }
        public IDataManager DataManager { get; }

        public IActionResult Index()
        {
            return this.View(new AdminModel());
        }
    }
}
