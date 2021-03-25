using Microsoft.AspNetCore.Mvc;
using NicheMarket.Services;
using NicheMarket.Web.Helpers;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        public CartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = await shoppingCartService.Total(cart);
            return View();
        }

        [Route("buy/{id}")]
        public async Task<IActionResult> Buy(string id)
        {
            Dictionary<string, List<ShoppingCartItem>> cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");
            cart = await shoppingCartService.AddRetailerIdToCart(cart, id);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            Dictionary<string, List<ShoppingCartItem>> cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");
            cart = await shoppingCartService.RemoveProduct(cart, id);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Decrease(string id)
        {
            Dictionary<string, List<ShoppingCartItem>> cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");
            cart = await shoppingCartService.Decrease(cart, id);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Increase(string id)
        {
            Dictionary<string, List<ShoppingCartItem>> cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");
            cart = await shoppingCartService.Increase(cart, id);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

    }
}
