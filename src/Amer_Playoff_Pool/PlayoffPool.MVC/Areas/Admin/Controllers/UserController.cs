namespace PlayoffPool.MVC.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PlayoffPool.MVC.Areas.Admin.Models;
    using PlayoffPool.MVC.Areas.Admin.ViewModels;
    using PlayoffPool.MVC.Controllers;
    using PlayoffPool.MVC.Extensions;
    using PlayoffPool.MVC.Helpers;
    using PlayoffPool.MVC.Models;

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
        public IActionResult Index()
        {
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
            UserViewModel viewModel = GenerateViewModel(model);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(UserModel model)
        {
            return this.RedirectToAction("Index");
        }

        private static UserViewModel GenerateViewModel(UserModel model)
        {
            UserViewModel viewModel = new UserViewModel()
            {
                UserModel = model,
            };

            viewModel.AddBreadcrumb($"{model.LastName}, {model.FirstName}");
            return viewModel;
        }
    }
}
