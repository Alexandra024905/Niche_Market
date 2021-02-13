using AutoMapperConfiguration;
using Microsoft.AspNetCore.Mvc;
using NicheMarket.Services;
using NicheMarket.Services.Models;
using NicheMarket.Web.Helpers;
using NicheMarket.Web.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IOrderService orderService;
        public ClientController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Add shopping cart
        [HttpGet]
        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CreateOrderBindingModel createOrderBindingModel)
        {
            OrderServiceModel orderServiceModel = createOrderBindingModel.To<OrderServiceModel>();
            orderServiceModel.ClientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<ShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(HttpContext.Session, "cart");
            foreach (var item in cart)
            {
                orderServiceModel.Products.Add(item.Product.Id);
            }
            bool result = await orderService.CreateOrder(orderServiceModel);
            return Redirect("Order");
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            return View();
        }
    }
}
