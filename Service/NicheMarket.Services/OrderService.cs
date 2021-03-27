using AutoMapperConfiguration;
using Microsoft.EntityFrameworkCore;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Services.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public class OrderService : IOrderService
    {
        private readonly NicheMarketDBContext dBContext;
        public OrderService(NicheMarketDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<bool> CreateOrder(Dictionary<string, List<ShoppingCartItem>> cart, OrderServiceModel orderServiceModel)
        {
            foreach (var retailerId in cart.Keys)
            {
                Order newOrder = new Order
                {
                    Id = Guid.NewGuid().ToString(),
                    RetailerId = retailerId,
                    Adress = orderServiceModel.Adress,
                    ClientName = orderServiceModel.ClientName,
                    ClientId = orderServiceModel.ClientId,
                    IsCompleted = false,
                    Products = await GetMyProducts(cart, retailerId),
                    TotalPrice = CalculateTotalPrice(cart, retailerId)
                };
                bool result = await this.dBContext.AddAsync(newOrder) != null;
            }
            await this.dBContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrderViewModel>> MyOrders(string clinetId)
        {
            List<OrderViewModel> myOrders = await dBContext.Orders
                .Where(o => o.ClientId == clinetId)
                .Select(o => o.To<OrderViewModel>())
                .ToListAsync();

            return myOrders;
        }

        public async Task<bool> DeleteOrder(string id)
        {
            bool result = false;
            if (id != null)
            {
                Order orderToRemove = await dBContext.Orders.Include(p => p.Products).FirstOrDefaultAsync();
                orderToRemove.Products.Clear();
                dBContext.Orders.Remove(orderToRemove);
                dBContext.SaveChanges();
                result = true;
            }
            return result;
        }

        private decimal CalculateTotalPrice(Dictionary<string, List<ShoppingCartItem>> cart, string retailerId)
        {
            decimal price = 0;
            foreach (var cartItem in cart[retailerId])
            {
                price += cartItem.Product.Price * cartItem.Quantity;
            }
            return price;
        }
        private async Task<List<Product>> GetMyProducts(Dictionary<string, List<ShoppingCartItem>> cart, string retailerId)
        {
            List<Product> products = new List<Product>();
            foreach (var cartItem in cart[retailerId])
            {
                products.Add(await dBContext.Products.FirstOrDefaultAsync(p => p.Id == cartItem.Product.Id));
            }
            return products;
        }

        private async Task<Order> FindOrderById(string id)
        {
            return await dBContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
