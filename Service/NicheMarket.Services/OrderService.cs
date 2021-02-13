using AutoMapperConfiguration;
using NicheMarket.Data;
using NicheMarket.Data.Models;
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
    public class OrderService : IOrderService
    {
        private readonly NicheMarketDBContext dBContext;
        public OrderService(NicheMarketDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<bool> CreateOrder(OrderServiceModel orderServiceModel)
        {
            Order newOrder = new Order
            {
                Id = Guid.NewGuid().ToString(),
                Adress = orderServiceModel.Adress,
                ClientName = orderServiceModel.ClientName,
                ClientId = orderServiceModel.ClientId,
                IsCompleted = false,
                Products = GetMyProducts(orderServiceModel.Products)
            };
            Product firstProduct = newOrder.Products.FirstOrDefault();
            if (firstProduct != null)
            {
                newOrder.RetailerId = firstProduct.RetailerId;
                newOrder.TotalPrice = CalculateTotalPrice(newOrder.Products);
            }
            bool result = await this.dBContext.AddAsync(newOrder) != null;
            await this.dBContext.SaveChangesAsync();
            return result;
        }

        public async Task<List<OrderViewModel>> MyOrders(string clinetId)
        {
            List<Order> orders = dBContext.Orders.Where(o => o.ClientId == clinetId).ToList();
            List<OrderViewModel> myOrders = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                myOrders.Add(order.To<OrderViewModel>());
            }
            return myOrders;
        }

        public async Task<bool> DeleteOrder(string id)
        {
            bool result = false;
            if (id != null)
            {
                dBContext.Orders.Remove(FindOrderById(id));
                dBContext.SaveChanges();
                result = true;

            }
            return result;
        }

        private double CalculateTotalPrice(List<Product> products)
        {
            double price = 0;
            foreach (var product in products)
            {
                price += product.Price;
            }
            return price;
        }
        private List<Product> GetMyProducts(List<string> productsIds)
        {
            List<Product> products = new List<Product>();
            foreach (var id in productsIds)
            {
                products.Add(dBContext.Products.FirstOrDefault(p => p.Id == id));
            }
            return products;
        }

        private Order FindOrderById(string id)
        {
            return dBContext.Orders.FirstOrDefault(o => o.Id == id);
        }
    }
}
