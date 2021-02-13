using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public interface IRetailerService
    {
        Task<IEnumerable<ProductViewModel>> MyProducts(string retailerId);

        Task<IEnumerable<OrderViewModel>> RetailerOrders(string retailerId);
    }
}
