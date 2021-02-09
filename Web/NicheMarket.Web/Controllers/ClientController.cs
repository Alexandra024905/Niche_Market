using Microsoft.AspNetCore.Mvc;
using NicheMarket.Web.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{
    public class ClientController : Controller
    {

        public ClientController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PlaceOrder()
        {
            return  View();
        }        

        public async Task<IActionResult> PlaceOrder(CreateOrderBindingModel createOrderBindingModel)
        {
            //Add Order service
            return View();
        }
    }
}
