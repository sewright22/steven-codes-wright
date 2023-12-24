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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new CreateSeasonViewModel()
            {
                Season = new SeasonModel()
                {
                    Id = 0,
                    Year = DateTime.Now.Year.ToString(),
                },
            };

            return PartialView(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateSeasonViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return PartialView(model);
            }

            await this.DataManager.DataContext.CreateSeason(model.Season);

            return RedirectToAction(nameof(Index));
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

        [HttpGet]
        public IActionResult Round(int id)
        {
            var model = new RoundModel()
            { 
                Name = string.Empty,
                PlayoffId = id,
            };

            model.Rounds.AddRange(this.DataManager.DataContext.GetRounds().ToList());

            return View(model);
        }
    }
}
