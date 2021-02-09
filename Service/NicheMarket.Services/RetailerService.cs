using AutoMapperConfiguration;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public class RetailerService : IRetailerService
    {
        private readonly NicheMarketDBContext dBContext;

        public RetailerService(NicheMarketDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IEnumerable<ProductViewModel>> MyProducts(string retailerId)
        {
            List<Product> products  =  dBContext.Products.Where(p => p.RetailerId == retailerId).ToList();
            List<ProductViewModel> myProducts  =  new List<ProductViewModel>();

            foreach (var product in products)
            {
                myProducts.Add( product.To<ProductViewModel>());
            }
            return myProducts;
        }

        
    }
}
