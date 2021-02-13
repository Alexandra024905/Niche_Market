using NicheMarket.Services.Models;
using NicheMarket.Web.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(OrderServiceModel orderServiceModel);
        Task<bool> DeleteOrder(string id);
    }
}
