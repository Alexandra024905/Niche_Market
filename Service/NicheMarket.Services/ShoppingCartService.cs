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

        public async Task<Dictionary<string, List<ShoppingCartItem>>> AddRetailerIdToCart(Dictionary<string, List<ShoppingCartItem>> cart, ProductViewModel productViewModel)
        {
            if (cart.ContainsKey(productViewModel.RetailerId))
            {
                await AddProductToCart(cart, productViewModel);
            }
            else
            {
                cart.Add(productViewModel.RetailerId, new List<ShoppingCartItem>());
                cart[productViewModel.RetailerId].Add(new ShoppingCartItem { Product = productViewModel, Quantity = 1 });
            }

            return cart;
        }
        public async Task<bool> AddProductToCart(Dictionary<string, List<ShoppingCartItem>> cart, ProductViewModel productViewModel)
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
            return exists;
        }
        public async Task<ProductViewModel> Find(string id)
        {
            Product product = await dBContext.Products.FindAsync(id);
            return product.To<ProductViewModel>();
        }


    }
}
