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
        public Task<Dictionary<string, List<ShoppingCartItem>>> AddRetailerIdToCart(Dictionary<string, List<ShoppingCartItem>> cart, string id);
        public Task<Dictionary<string, List<ShoppingCartItem>>> AddProductToCart(Dictionary<string, List<ShoppingCartItem>> cart, ProductViewModel productViewModel);
        public Task<Dictionary<string, List<ShoppingCartItem>>> RemoveProduct(Dictionary<string, List<ShoppingCartItem>> cart, string id);
        public Task<Dictionary<string, List<ShoppingCartItem>>> Increase(Dictionary<string, List<ShoppingCartItem>> cart, string id);
        public Task<Dictionary<string, List<ShoppingCartItem>>> Decrease(Dictionary<string, List<ShoppingCartItem>> cart, string id);
        public Task<double> Total(Dictionary<string, List<ShoppingCartItem>> cart);

        //public Task<ProductViewModel> Find(string id);
    }
}
