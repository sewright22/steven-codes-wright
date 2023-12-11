namespace PlayoffPool.MVC.Controllers
{
    using AmerFamilyPlayoffs.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using PlayoffPool.MVC.Helpers;
    using PlayoffPool.MVC.Models;
    using System;
    using static PlayoffPool.MVC.Constants;

    public class UserController : Controller
    {
        public UserController(ILogger<UserController> logger, IDataManager dataManager)
        {
            this.Logger = logger;
            this.DataManager = dataManager;
        }

        public ILogger<UserController> Logger { get; }
        public IDataManager DataManager { get; }
    }
}
