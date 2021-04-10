using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NicheMarket.Data.Models.Users;
using NicheMarket.Services;
using NicheMarket.Web.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly SignInManager<NicheMarketUser> singInManagerUser;
        private readonly UserManager<NicheMarketUser> userManager;
        private readonly IUserService userService;

        public UserController(ILogger<HomeController> logger, SignInManager<NicheMarketUser> singInManagerUser, UserManager<NicheMarketUser> userManager, IUserService userService)
        {
            this.logger = logger;
            this.singInManagerUser = singInManagerUser;
            this.userManager = userManager;
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProfileDetails()
        {
            NicheMarketUser user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }
            else
            {
                return View("EditUserInfo", userService.ProfileDetails(user));
            }
        }

        public async Task<IActionResult> EditProfil(UserBindingModel userBindingModel)
        {
            NicheMarketUser user = await userManager.GetUserAsync(User);
            await singInManagerUser.RefreshSignInAsync(await userService.EditProfil(userBindingModel, user));
            logger.LogInformation("User changed their password successfully.");
            return View("EditUserInfo", userService.ProfileDetails(user));
        }

        public async Task<IActionResult> ChangeRole()
        {
            NicheMarketUser user = await userManager.GetUserAsync(User);
            return View("EditUserInfo", await userService.ChangeRole(user));
        }

    }
}
