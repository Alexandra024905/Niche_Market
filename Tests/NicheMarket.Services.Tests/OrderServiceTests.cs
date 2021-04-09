using AutoMapperConfiguration;
using Microsoft.EntityFrameworkCore;
using Moq;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Web.Models.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services.Tests
{
    [TestFixture]
    class OrderServiceTests
    {
        private NicheMarketDBContext dBContext;
        private IOrderService orderService;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(Order).Assembly.GetTypes(),
                typeof(OrderViewModel).Assembly.GetTypes()
                );

            DbContextOptions<NicheMarketDBContext> options = new DbContextOptionsBuilder<NicheMarketDBContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid()}")
                .Options;

            this.dBContext = new NicheMarketDBContext(options);
            this.orderService = new OrderService(dBContext);
        }

        [Test]
        public async Task MyOrders_ValidData_ShouldReturnListOfProductViewModels()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            Assert.IsNotEmpty(await orderService.MyOrders(userId), TestsMessages.ResultErrorMessage(nameof(orderService.MyOrders)));
            Assert.IsNotNull(await orderService.MyOrders(userId), TestsMessages.ResultErrorMessage(nameof(orderService.MyOrders)));
        }

        [Test]
        public async Task MyOrders_ValidData_ShouldReturnTruthyValue()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            Assert.AreEqual(actualOrderss.Count, expectedOrders.Count, TestsMessages.ResultErrorMessage(nameof(orderService.MyOrders)));
        }

        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualIdValues()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Id, expectedOrders[i].Id, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.Id)));
            }

        }

        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualRetailerIdValues()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].RetailerId, expectedOrders[i].RetailerId, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.RetailerId)));
            }

        }

        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualClientIdValues()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].ClientId, expectedOrders[i].ClientId, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.ClientId)));
            }
        }


        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualClientNameValues()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].ClientName, expectedOrders[i].ClientName, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.ClientName)));
            }
        }


        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualAdressValues()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Adress, expectedOrders[i].Adress, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.Adress)));
            }
        }


        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualIsCompletedValues()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].IsCompleted, expectedOrders[i].IsCompleted, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.IsCompleted)));
            }
        }
        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualTotalPriceValues()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].TotalPrice, expectedOrders[i].TotalPrice, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.TotalPrice)));
            }
        }


        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualProductsCount()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrderss = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Products.Count, expectedOrders[i].Products.Count, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.Products)));
            }
        }


        [Test]
        public async Task MyOrders_ValidData_ShouldReturnEntitiesWithEqualProductsValues()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Mock<Product> mockProduct = new Mock<Product>();
            mockProduct.Object.Id = "ProductId";
            mockProduct.Object.Price = 5;
            Mock<OrderItem> mockOrderItem = new Mock<OrderItem>();
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>(),
                IsCompleted = false,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = (dBContext.Orders).ToList();

            List<OrderViewModel> actualOrders = await orderService.MyOrders(userId);

            for (int i = 0; i < actualOrders.Count; i++)
            {
                for (int j = 0; j < actualOrders[i].Products.Count; j++)
                {
                    Assert.AreEqual(actualOrders[i].Products[j].Id, expectedOrders[i].Products[j].ProductId, TestsMessages.MappingErrorMessage(nameof(orderService.MyOrders), nameof(Order.Products)));
                }
            }

        }



        [TearDown]
        public void Dispose()
        {
            this.dBContext.Dispose();
            this.orderService = null;
        }

    }
}
