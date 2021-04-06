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
                    TotalPrice = await CalculateTotalPrice(cart, retailerId)
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

        public async Task<OrderViewModel> DetailsOrder(string id)
        {
            Order order = await dBContext.Orders.Where(o => o.Id == id).Include(o => o.Products).FirstOrDefaultAsync();
            OrderViewModel orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                ClientName = order.ClientName,
                Adress = order.Adress,
                TotalPrice = order.TotalPrice,
                IsCompleted = order.IsCompleted,
                Products = await  FindProducts(order.Products)
            };
            return orderViewModel;
        }
        private async Task<decimal> CalculateTotalPrice(Dictionary<string, List<ShoppingCartItem>> cart, string retailerId)
        {
            decimal price = 0;
            foreach (var cartItem in cart[retailerId])
            {
                price += cartItem.Product.Price * cartItem.Quantity;
            }
            return price;
        }
        private async Task<List<OrderItem>> GetMyProducts(Dictionary<string, List<ShoppingCartItem>> cart, string retailerId)
        {
            List<OrderItem> products = new List<OrderItem>();
            foreach (var cartItem in cart[retailerId])
            {
                OrderItem orderItem = new OrderItem
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = cartItem.Product.Id,
                    Quantity = cartItem.Quantity
                };
                await dBContext.OrderItems.AddAsync(orderItem);
                dBContext.SaveChanges();
                products.Add(orderItem);
            }
            return products;
        }

        public async Task<List<ProductViewModel>> FindProducts(List<OrderItem> orderItems)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            foreach (var orderItem in orderItems)
            {
                int quantity = orderItem.Quantity;
                while (quantity > 0)
                {
                    Product product = await dBContext.Products.FindAsync(orderItem.ProductId);
                    products.Add(product.To<ProductViewModel>());
                    quantity--;
                }
            }
            return products;
        }
    }
}
