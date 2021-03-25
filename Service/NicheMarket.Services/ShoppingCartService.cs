using AutoMapperConfiguration;
using Microsoft.AspNetCore.Http;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly NicheMarketDBContext dBContext;
        public ShoppingCartService(NicheMarketDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Dictionary<string, List<ShoppingCartItem>>> AddRetailerIdToCart(Dictionary<string, List<ShoppingCartItem>> cart, string id)
        {
            ProductViewModel productViewModel = await Find(id);
            if (cart == null)
            {
                cart = new Dictionary<string, List<ShoppingCartItem>>();
                cart.Add(productViewModel.RetailerId, new List<ShoppingCartItem>() { new ShoppingCartItem { Product = productViewModel, Quantity = 1 } });
                return cart;
            }
            else if (cart.ContainsKey(productViewModel.RetailerId))
            {
                return await AddProductToCart(cart, productViewModel);
            }
            else
            {
                cart.Add(productViewModel.RetailerId, new List<ShoppingCartItem>());
                cart[productViewModel.RetailerId].Add(new ShoppingCartItem { Product = productViewModel, Quantity = 1 });
                return cart;
            }
        }
        public async Task<Dictionary<string, List<ShoppingCartItem>>> AddProductToCart(Dictionary<string, List<ShoppingCartItem>> cart, ProductViewModel productViewModel)
        {
            bool exists = false;
            foreach (var item in cart[productViewModel.RetailerId])
            {
                if (item.Product.Id == productViewModel.Id)
                {
                    item.Quantity++;
                    exists = true;
                }
            }
            if (!exists)
            {
                cart[productViewModel.RetailerId].Add(new ShoppingCartItem { Product = productViewModel, Quantity = 1 });
            }
            return cart;
        }

        public async Task<Dictionary<string, List<ShoppingCartItem>>> RemoveProduct(Dictionary<string, List<ShoppingCartItem>> cart, string id)
        {
            ProductViewModel productViewModel = await Find(id);
            foreach (var item in cart[productViewModel.RetailerId])
            {
                if (item.Product.Id == productViewModel.Id)
                {
                    cart[productViewModel.RetailerId].Remove(item);
                    break;
                }
            }
            return cart;
        }

        public async Task<Dictionary<string, List<ShoppingCartItem>>> Decrease(Dictionary<string, List<ShoppingCartItem>> cart, string id)
        {
            ProductViewModel productViewModel = await Find(id);
            foreach (var item in cart[productViewModel.RetailerId])
            {
                if (item.Product.Id == productViewModel.Id)
                {
                    if (item.Quantity == 1)
                    {
                        cart[productViewModel.RetailerId].Remove(item);
                        break;
                    }
                    else
                    {
                        item.Quantity--;
                    }
                }
            }
            return cart;
        }
        public async Task<Dictionary<string, List<ShoppingCartItem>>> Increase(Dictionary<string, List<ShoppingCartItem>> cart, string id)
        {
            ProductViewModel productViewModel = await Find(id);
            foreach (var item in cart[productViewModel.RetailerId])
            {
                if (item.Product.Id == productViewModel.Id)
                {
                    item.Quantity++;
                }
            }
            return cart;
        }


        public async Task<double> Total(Dictionary<string, List<ShoppingCartItem>> cart)
        {
            double sum = 0;
            foreach (var list in cart.Values)
            {
                foreach (var item in list)
                {
                    sum += (double)item.Product.Price*item.Quantity;
                }
            }
            return sum;
        }

        public async Task<ProductViewModel> Find(string id)
        {
            Product product = await dBContext.Products.FindAsync(id);
            return product.To<ProductViewModel>();
        }
    }
}
