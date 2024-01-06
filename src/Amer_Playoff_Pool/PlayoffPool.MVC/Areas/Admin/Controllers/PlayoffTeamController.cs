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
    public class PlayoffTeamController : Controller
    {
        public PlayoffTeamController(ILogger<PlayoffTeamController> logger, IDataManager dataManager)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.Logger = logger;
            this.DataManager = dataManager;
        }

        public ILogger<PlayoffTeamController> Logger { get; }
        public IDataManager DataManager { get; }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int id)
        {
            var model = new PlayoffTeamModel()
            {
                PlayoffId = id,
                Name = string.Empty,
            };

            model.SeasonId = this.DataManager.DataContext.GetSeasonIdFromPlayoffId(id);
            model.Conferences.AddRange(this.DataManager.DataContext.GetConferences().OrderBy(x => x.Text).ToList());
            model.Teams.AddRange(this.DataManager.DataContext.GetTeams().AsSelectListItems().OrderBy(x => x.Text).ToList());

            return this.PartialView(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(PlayoffTeamModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            this.DataManager.DataContext.AddPlayoffTeam(model);

            return this.RedirectToAction(nameof(SeasonController.Edit), nameof(SeasonController).Replace("Controller", string.Empty), new { area = "Admin", id = model.SeasonId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = this.DataManager.DataContext.GetPlayoffRound(id);

            return this.PartialView(model);
        }

        [HttpPost]
        public IActionResult Edit(RoundModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            this.DataManager.DataContext.UpdatePlayoffRound(model);

            return this.RedirectToAction(nameof(SeasonController.Edit), nameof(SeasonController).Replace("Controller", string.Empty), new { area = "Admin", id = model.SeasonId });
        }
    }
}
