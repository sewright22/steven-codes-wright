namespace PlayoffPool.MVC.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Helpers;

    [Area("Admin")]
    public class PlayoffController : Controller
    {
        public PlayoffController(ILogger<PlayoffController> logger, IDataManager dataManager)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.Logger = logger;
            this.DataManager = dataManager;
        }

        public ILogger<PlayoffController> Logger { get; }
        public IDataManager DataManager { get; }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            SeasonModel model = this.DataManager.DataContext.GetSeasonFromPlayoffId(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(SeasonModel model)
        {
            if (model.CutoffDateTime.HasValue == false)
            {
                return View(model);
            }

            this.DataManager.DataContext.UpdatePlayoffStartTime(model.PlayoffId, model.CutoffDateTime);

            return RedirectToAction("Index", "Season");
        }
    }
}
