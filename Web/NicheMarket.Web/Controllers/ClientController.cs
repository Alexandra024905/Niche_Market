using AutoMapperConfiguration;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            Dictionary<string, List<ShoppingCartItem>> cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");
            bool result = await orderService.CreateOrder(cart, orderServiceModel);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", new Dictionary<string, List<ShoppingCartItem>>());
            return Redirect("MyOrders");
        }

        public async Task<IActionResult> MyOrders()
        {
            return View(await orderService.MyOrders(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            await orderService.DeleteOrder(orderId);
            return Redirect("/");
        }

        public async Task<IActionResult> OrderDetails(string orderId)
        {
            return View(await orderService.OrderDetails(orderId));
        }
    }
}
