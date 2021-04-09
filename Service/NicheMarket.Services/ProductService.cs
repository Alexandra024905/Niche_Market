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
using NicheMarket.Data.Models.Users;
using System.Security.Claims;

namespace NicheMarket.Services
{
    public class ProductService : IProductService
    {

        private readonly NicheMarketDBContext dBContext;

        public ProductService(NicheMarketDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IEnumerable<ProductViewModel>> AllProducts()
        {
            IEnumerable<ProductViewModel> products = await dBContext.Products
                .Select(p => p.To<ProductViewModel>())
                .ToListAsync();

            return products;
        }

        public async Task<bool> CreateProduct(ProductServiceModel productServiceModel)
        {
            Product newProduct = productServiceModel.To<Product>();
            newProduct.Id = Guid.NewGuid().ToString();
            if (productServiceModel.ImageURL == null)
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
                if (await ProductExists(id))
                {
                    Product product = dBContext.Products.Find(id);

                    await DeleteProductFromAllOrders(product);
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
                if (await ProductExists(productServiceModel.Id))
                {
                    Product product = await FindProduct(productServiceModel.Id);
                    product.Price = productServiceModel.Price;
                    product.Title = productServiceModel.Title;
                    product.Description = productServiceModel.Description;
                    product.Type = productServiceModel.Type;
                    if (productServiceModel.ImageURL != null)
                    {
                        product.ImageURL = productServiceModel.ImageURL;
                    }
                    dBContext.Products.Update(product);
                    dBContext.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        private async Task<Product> FindProduct(string id)
        {
            return await dBContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        private async Task<ProductViewModel> Find(string id)
        {
            Product product = await FindProduct(id);
            return product.To<ProductViewModel>();
        }

        private List<Category> ProductCategories(List<string> categoriesNames)
        {
            List<Category> categories = new List<Category>();
            foreach (var categorie in categoriesNames)
            {
                categories.Add(dBContext.Category.Where(c => c.Name == categorie).FirstOrDefault());
            }
            return categories;
        }

        private async Task<bool> ProductExists(string id)
        {
            return await dBContext.Products.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> DeleteProductFromAllOrders(Product productToRemove)
        {
            foreach (var orderItem in dBContext.OrderItems.Where(oi => oi.ProductId == productToRemove.Id))
            {
                IEnumerable<Order> orders = dBContext.Orders.Include("Products").Where(o => o.Products.Contains(orderItem));
                await EditOrdersWithOrderItem(orders, orderItem);
                dBContext.Remove(orderItem);
            }
            dBContext.OrderItems.RemoveRange(dBContext.OrderItems.Where(oi => oi.ProductId == productToRemove.Id));
            bool result = dBContext.Products.Remove(productToRemove)!=null;
            dBContext.SaveChanges();
            return result;
        }

        private async Task<bool> EditOrdersWithOrderItem(IEnumerable<Order> orders, OrderItem orderItem)
        {
            foreach (var order in orders)
            {
                order.Products.Remove(orderItem);
                if (order.Products.Count > 0)
                {
                    order.TotalPrice = await CalculateNewPrice(order);
                }
                else
                {
                    dBContext.Orders.Remove(order);
                }
            }
            return true;
        }

        public async Task<decimal> CalculateNewPrice(Order order)
        {
            decimal price = 0;
            foreach (var orderItem in order.Products)
            {
                Product product = await dBContext.Products.FindAsync(orderItem.ProductId);
                price += product.Price * orderItem.Quantity;
            }
            return price;
        }
    }
}
