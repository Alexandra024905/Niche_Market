using NicheMarket.Services.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(Dictionary<string, List<ShoppingCartItem>> cart, OrderServiceModel orderServiceModel);
        Task<List<OrderViewModel>> MyOrders(string clinetId);
        Task<OrderViewModel> DetailsOrder(string id);
        Task<bool> DeleteOrder(string id);
    }
}
