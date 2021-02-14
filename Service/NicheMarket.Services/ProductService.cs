using NicheMarket.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using AutoMapperConfiguration;
using Microsoft.EntityFrameworkCore;
using NicheMarket.Web.Models.BindingModels;
using System.Linq;
using NicheMarket.Web.Models.ViewModels;

namespace NicheMarket.Services
{
    public class ProductService : IProductService
    {

        private readonly NicheMarketDBContext dBContext;

        public ProductService(NicheMarketDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<List<ProductViewModel>> AllProducts()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            foreach (var product in dBContext.Products)
            {
                products.Add(product.To<ProductViewModel>());
            }
            return products;
        }

        public async Task<bool> CreateProduct(ProductServiceModel productServiceModel)
        {
            Product newProduct = productServiceModel.To<Product>();

            newProduct.Id = Guid.NewGuid().ToString();
            if (productServiceModel.ImageURL == "")
            {
                newProduct.ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png";
            }
            bool result = await this.dBContext.AddAsync(newProduct) != null;

            await this.dBContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            bool result = false;
            if (id != null)
            {
                if (ProductExists(id))
                {
                    Product product = await FindProduct(id);
                    dBContext.Products.Remove(product);
                    dBContext.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        public async Task<ProductViewModel> ProductDetails(string id)
        {
            Product product = await dBContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            return product.To<ProductViewModel>();
        }
        public async Task<ProductBindingModel> GetProduct(string id)
        {
            Product product = await dBContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            return product.To<ProductBindingModel>();
        }

        public async Task<bool> EditProduct(ProductServiceModel productServiceModel)
        {
            bool result = false;
            if (productServiceModel.Id != null)
            {
                if (ProductExists(productServiceModel.Id))
                {
                    Product product = productServiceModel.To<Product>();
                    dBContext.Products.Update(product);
                    dBContext.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        public async Task<Product> FindProduct(string id)
        {
            return await dBContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        } 
        public async Task<ProductViewModel> Find(string id)
        {
            Product product =  await FindProduct(id);
            return  product.To<ProductViewModel>();
        }

        private bool ProductExists(string id)
        {
            return dBContext.Products.Any(e => e.Id == id);
        }
    }
}
