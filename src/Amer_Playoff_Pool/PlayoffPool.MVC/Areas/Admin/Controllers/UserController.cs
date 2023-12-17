namespace PlayoffPool.MVC.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Controllers;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Helpers;

    [Area("Admin")]
    public class UserController : Controller
    {
        public UserController(ILogger<AdminController> logger, IDataManager dataManager)
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

        [HttpGet]
        [Authorize]
        public IActionResult Index(string? id)
        {
            if (id.HasValue())
            {
                return this.RedirectToAction(nameof(this.Edit));
            }

            var model = new UsersModel();

            var users = this.DataManager.DataContext.GetUsers();

            model.Users.AddRange(users);

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(string? id)
        {

            UserModel model = this.DataManager.DataContext.GetUser(id);

            return this.View(model);
        }
    }
}
