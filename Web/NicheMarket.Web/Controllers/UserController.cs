using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly SignInManager<IdentityUser> singInManagerUser;

        public UserController(ILogger<HomeController> logger, SignInManager<IdentityUser> singInManagerUser)
        {
            this.logger = logger;
            this.singInManagerUser = singInManagerUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> Login (IdentityUserLogin model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //}
    }
}
