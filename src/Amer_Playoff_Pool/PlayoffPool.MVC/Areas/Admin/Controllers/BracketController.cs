namespace PlayoffPool.MVC.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Areas.Admin.ViewModels;
    using PlayoffPool.MVC.Controllers;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Helpers;

    [Area("Admin")]
    public class BracketController : Controller
    {
        public BracketController(ILogger<BracketController> logger, IDataManager dataManager)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.Logger = logger;
            this.DataManager = dataManager;
        }

        public ILogger<BracketController> Logger { get; }
        public IDataManager DataManager { get; }
        public IActionResult Seasons()
        {
            BracketsModel model = new BracketsModel();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Seasons(int year)
        {
            var model = new BracketsModel();

            model.Brackets.AddRange(this.DataManager.DataContext.GetBracketsForYear(year).ToList());

            return PartialView(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new UpdateSeasonViewModel()
            {
                Season = this.DataManager.DataContext.GetSeason(id),
            };

            model.AddBreadcrumb(model.Season.Year);

            return View(model);
        }
    }
}
