namespace PlayoffPool.MVC.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Controllers;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Helpers;

    [Area("Admin")]
    public class SeasonController : Controller
    {
        public SeasonController(ILogger<AdminController> logger, IDataManager dataManager)
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
            SeasonsModel model = new SeasonsModel();

            model.Seasons.AddRange(this.DataManager.DataContext.GetSeasons());

            return View(model);
        }
    }
}
