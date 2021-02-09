using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NicheMarket.Data.Models.Users;
using NicheMarket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{
    public class RetailerController : Controller
    {
        private readonly IProductService productService;
        private readonly IRetailerService retailerService;
        private readonly IUserService userService; // edit my role

        private readonly UserManager<NicheMarketUser> userManager;


        public RetailerController(IProductService productService, IUserService userService, IRetailerService retailerService, UserManager<NicheMarketUser> userManager)
        {
            this.productService = productService;
            this.userService = userService;
            this.retailerService = retailerService;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult>RetailerProducts()
        {
            return View(await retailerService.MyProducts(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            //return View();
        }



    }
}
