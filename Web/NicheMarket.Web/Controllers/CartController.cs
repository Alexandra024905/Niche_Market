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
        private readonly IProductService productService;
        private readonly IShoppingCartService shoppingCartService;
        public CartController(IProductService productService, IShoppingCartService shoppingCartService)
        {
            this.productService = productService;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            //ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            ViewBag.total = cart.Count;
            return View();
        }

        [Route("buy/{id}")]
        public async Task<IActionResult> Buy(string id)
        {
            ProductViewModel productViewModel = await productService.Find(id);
            if (SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart") == null)
            {
                Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
                cart.Add(productViewModel.RetailerId, new List<ShoppingCartItem>());
                cart[productViewModel.RetailerId].Add(new ShoppingCartItem { Product = productViewModel, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                Dictionary<string, List<ShoppingCartItem>> cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");

                if (cart.ContainsKey(productViewModel.RetailerId))
                {
                    bool exists = false;
                    foreach (var item in cart[productViewModel.RetailerId])
                    {
                        if (item.Product == productViewModel)
                        {
                            item.Quantity++;
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        cart[productViewModel.RetailerId].Add(new ShoppingCartItem { Product = productViewModel, Quantity = 1 });
                    }
                }
                else
                {
                    cart.Add(productViewModel.RetailerId, new List<ShoppingCartItem>());
                    cart[productViewModel.RetailerId].Add(new ShoppingCartItem { Product = productViewModel, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public void AddRetailerIdToCart(ProductViewModel productViewModel)
        {
            Dictionary<string, List<ShoppingCartItem>> cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");

            if (cart.ContainsKey(productViewModel.RetailerId))
            {
                AddProductToCart(cart, productViewModel);
            }
            else
            {
                cart.Add(productViewModel.RetailerId, new List<ShoppingCartItem>());
                cart[productViewModel.RetailerId].Add(new ShoppingCartItem { Product = productViewModel, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }

        public void AddProductToCart(Dictionary<string, List<ShoppingCartItem>> cart, ProductViewModel productViewModel)
        {
            bool exists = false;
            foreach (var item in cart[productViewModel.RetailerId])
            {
                if (item.Product == productViewModel)
                {
                    item.Quantity++;
                    exists = true;
                }
            }
            if (!exists)
            {
                cart[productViewModel.RetailerId].Add(new ShoppingCartItem { Product = productViewModel, Quantity = 1 });
            }
        }

        [Route("remove/{id}")]
        public IActionResult Remove(ShoppingCartItem shoppingCartItem)
        {
            Dictionary<string, List<ShoppingCartItem>> cart = SessionHelper.GetObjectFromJson<Dictionary<string, List<ShoppingCartItem>>>(HttpContext.Session, "cart");
            cart[shoppingCartItem.Product.RetailerId].Remove(shoppingCartItem);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }




    }
}
