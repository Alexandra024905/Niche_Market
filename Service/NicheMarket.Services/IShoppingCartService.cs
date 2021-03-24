using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public interface IShoppingCartService
    {
        public Task<Dictionary<string, List<ShoppingCartItem>>> AddRetailerIdToCart(Dictionary<string, List<ShoppingCartItem>> cart, ProductViewModel productViewModel);
        public Task<bool> AddProductToCart(Dictionary<string, List<ShoppingCartItem>> cart, ProductViewModel productViewModel);
    }
}
