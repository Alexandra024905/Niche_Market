using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Retailer")]
    public class RetailerController : Controller
    {
        private readonly IRetailerService retailerService;



        public RetailerController( IRetailerService retailerService)
        {
            this.retailerService = retailerService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult>RetailerProducts()
        {
            return View(await retailerService.MyProducts(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }      
        
        public async Task<IActionResult>RetailerOrders()
        {
            return View(await retailerService.RetailerOrders(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        

    }
}
