using AutoMapperConfiguration;
using Microsoft.EntityFrameworkCore;
using Moq;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Services.Models;
using NicheMarket.Web.Models.BindingModels;
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
                typeof(OrderViewModel).Assembly.GetTypes(),
                typeof(OrderServiceModel).Assembly.GetTypes()
                );

            DbContextOptions<NicheMarketDBContext> options = new DbContextOptionsBuilder<NicheMarketDBContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid()}")
                .Options;

            this.dBContext = new NicheMarketDBContext(options);
            this.orderService = new OrderService(dBContext);
        }

        [Test]
        public async Task CreateOrder_ValidData_ShouldReturnTruthyValue()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            bool result = await orderService.CreateOrder(cart, orderServiceModel);
            Assert.True(result, TestsMessages.ResultErrorMessage(nameof(orderService.CreateOrder)));
        }

        [Test]
        public async Task CreateProduct_ValidData_ShouldCorrectlyCreateEntity()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            bool result = await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();

            Assert.IsNotNull(actualEntity, TestsMessages.ResultErrorMessage(nameof(orderService.CreateOrder)));
        }

        [Test]
        public async Task CreateProductWithCorrectAdress_ValidData_ShouldCorrectlyMapData()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            Order expectedEntity = new Order()
            {
                Adress = orderServiceModel.Adress,
                ClientId = orderServiceModel.ClientId,
                ClientName = orderServiceModel.ClientName,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = product.Price,
                Products = new List<OrderItem>() { new OrderItem() { ProductId = cart["1"][0].Product.Id, Quantity = cart["1"][0].Quantity } }
            };

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();


            Assert.AreEqual(expectedEntity.Adress, actualEntity.Adress, TestsMessages.MappingErrorMessage(nameof(orderService.CreateOrder), nameof(actualEntity.Adress)));
        }

        [Test]
        public async Task CreateProductWithCorrectClientId_ValidData_ShouldCorrectlyMapData()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            Order expectedEntity = new Order()
            {
                Adress = orderServiceModel.Adress,
                ClientId = orderServiceModel.ClientId,
                ClientName = orderServiceModel.ClientName,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = product.Price,
                Products = new List<OrderItem>() { new OrderItem() { ProductId = cart["1"][0].Product.Id, Quantity = cart["1"][0].Quantity } }
            };

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();


            Assert.AreEqual(expectedEntity.ClientId, actualEntity.ClientId, TestsMessages.MappingErrorMessage(nameof(orderService.CreateOrder), nameof(actualEntity.ClientId)));
        }

        [Test]
        public async Task CreateProductWithCorrectClientName_ValidData_ShouldCorrectlyMapData()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            Order expectedEntity = new Order()
            {
                Adress = orderServiceModel.Adress,
                ClientId = orderServiceModel.ClientId,
                ClientName = orderServiceModel.ClientName,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = product.Price,
                Products = new List<OrderItem>() { new OrderItem() { ProductId = cart["1"][0].Product.Id, Quantity = cart["1"][0].Quantity } }
            };

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();


            Assert.AreEqual(expectedEntity.ClientName, actualEntity.ClientName, TestsMessages.MappingErrorMessage(nameof(orderService.CreateOrder), nameof(actualEntity.ClientName)));
        }

        [Test]
        public async Task CreateProductWithCorrectIsCompleted_ValidData_ShouldCorrectlyMapData()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            Order expectedEntity = new Order()
            {
                Adress = orderServiceModel.Adress,
                ClientId = orderServiceModel.ClientId,
                ClientName = orderServiceModel.ClientName,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = product.Price,
                Products = new List<OrderItem>() { new OrderItem() { ProductId = cart["1"][0].Product.Id, Quantity = cart["1"][0].Quantity } }
            };

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();

            Assert.AreEqual(expectedEntity.IsCompleted, actualEntity.IsCompleted, TestsMessages.MappingErrorMessage(nameof(orderService.CreateOrder), nameof(actualEntity.IsCompleted)));
        }

        [Test]
        public async Task CreateProductWithCorrectRetailerId_ValidData_ShouldCorrectlyMapData()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("RetailerId", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            Order expectedEntity = new Order()
            {
                Adress = orderServiceModel.Adress,
                ClientId = orderServiceModel.ClientId,
                ClientName = orderServiceModel.ClientName,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = product.Price,
                Products = new List<OrderItem>() { new OrderItem() { ProductId = cart["RetailerId"][0].Product.Id, Quantity = cart["RetailerId"][0].Quantity } }
            };

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();


            Assert.AreEqual(expectedEntity.RetailerId, actualEntity.RetailerId, TestsMessages.MappingErrorMessage(nameof(orderService.CreateOrder), nameof(actualEntity.RetailerId)));
        }

        [Test]
        public async Task CreateProductWithCorrectTotalPrice_ValidData_ShouldCorrectlyMapData()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            Order expectedEntity = new Order()
            {
                Adress = orderServiceModel.Adress,
                ClientId = orderServiceModel.ClientId,
                ClientName = orderServiceModel.ClientName,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = product.Price,
                Products = new List<OrderItem>() { new OrderItem() { ProductId = cart["1"][0].Product.Id, Quantity = cart["1"][0].Quantity } }
            };

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();


            Assert.AreEqual(expectedEntity.TotalPrice, actualEntity.TotalPrice, TestsMessages.MappingErrorMessage(nameof(orderService.CreateOrder), nameof(actualEntity.TotalPrice)));
        }

        [Test]
        public async Task CreateProductWithCorrectProducts_ValidData_ShouldCorrectlyMapData()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            Order expectedEntity = new Order()
            {
                Adress = orderServiceModel.Adress,
                ClientId = orderServiceModel.ClientId,
                ClientName = orderServiceModel.ClientName,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = product.Price,
                Products = new List<OrderItem>() { new OrderItem() { ProductId = cart["1"][0].Product.Id, Quantity = cart["1"][0].Quantity } }
            };

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();

            for (int i = 0; i < actualEntity.Products.Count(); i++)
            {
                Assert.AreEqual(expectedEntity.Products[i].ProductId, actualEntity.Products[i].ProductId, TestsMessages.MappingErrorMessage(nameof(orderService.CreateOrder), nameof(actualEntity.Products)));
            }
        }

        [Test]
        public async Task CreateProductWithCorrectNumberOfPrpoducts_ValidData_ShouldCorrectlyMapData()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            Order expectedEntity = new Order()
            {
                Adress = orderServiceModel.Adress,
                ClientId = orderServiceModel.ClientId,
                ClientName = orderServiceModel.ClientName,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = product.Price,
                Products = new List<OrderItem>() { new OrderItem() { ProductId = cart["1"][0].Product.Id, Quantity = cart["1"][0].Quantity } }
            };

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();


            Assert.AreEqual(expectedEntity.Products.Count, actualEntity.Products.Count, TestsMessages.MappingErrorMessage(nameof(orderService.CreateOrder), nameof(actualEntity.Products)));
        }



        [Test]
        public async Task CreateProductSetIdCorrectly_ValidData_ShouldCorrectlySetProductId()
        {
            OrderServiceModel orderServiceModel = new OrderServiceModel() { Adress = "TestAdress", ClientName = "ClientName", ClientId = "ClientId" };
            Product product = new Product() { Id = "ProductId", Price = 5, RetailerId = "RetailerId" };
            Dictionary<string, List<ShoppingCartItem>> cart = new Dictionary<string, List<ShoppingCartItem>>();
            cart.Add("1", new List<ShoppingCartItem>() { new ShoppingCartItem() { Product = product.To<ProductViewModel>(), Quantity = 1 } });

            await orderService.CreateOrder(cart, orderServiceModel);
            Order actualEntity = await dBContext.Orders.FirstOrDefaultAsync();


            Assert.IsNotNull(actualEntity.Id, TestsMessages.SetPropertyIncorrectlyErrorMessage(nameof(orderService.CreateOrder), nameof(Order.Id)));
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


        [Test]
        public async Task DeleteOrder_ValidData_ShouldReturnTrue()
        {
            Order entity = new Order()
            {
                Id = "Id",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };

            dBContext.Orders.Add(entity);
            dBContext.SaveChanges();

            bool result = await orderService.DeleteOrder(entity.Id);

            Assert.IsTrue(result, TestsMessages.ResultErrorMessage(nameof(orderService.DeleteOrder)));
        }

        [Test]
        public async Task DeleteOrder_ValidData_ShouldCorrectlyDeleteEntity()
        {
            Order entity = new Order()
            {
                Id = "Id",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };

            dBContext.Orders.Add(entity);
            dBContext.SaveChanges();

            await orderService.DeleteOrder(entity.Id);

            List<Order> orders = dBContext.Orders.ToList();

            Assert.False(orders.Contains(entity), TestsMessages.ResultErrorMessage(nameof(orderService.DeleteOrder)));
        }

        [Test]
        public async Task DeleteOrderWithNullId_InvalidData_ShouldReturnFalse()
        {
            Order entity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };
            dBContext.Orders.Add(entity);
            dBContext.SaveChanges();

            bool result = await orderService.DeleteOrder(null);

            Assert.False(result, TestsMessages.ReturnsTrueWhenFalseIsExpected(nameof(orderService.DeleteOrder)));
        }

        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntity()
        {
            Order entity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };
            dBContext.Orders.Add(entity);
            dBContext.SaveChanges();

            OrderViewModel actualEntity = await orderService.OrderDetails(entity.Id);

            Assert.IsNotNull(actualEntity, TestsMessages.ResultErrorMessage(nameof(orderService.OrderDetails)));
        }

        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectAdress()
        {
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();

            OrderViewModel actualEntity = await orderService.OrderDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Adress, actualEntity.Adress, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.Adress)));
        }

        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectId()
        {
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();

            OrderViewModel actualEntity = await orderService.OrderDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Id, actualEntity.Id, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.Id)));
        }
        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectClientId()
        {
            Product product = new Product() { Id = "ProductId", Title = "Product", Price = 5 ,RetailerId="RetailerId"};
            OrderItem orderItem = new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 };
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() {  }
            };
            dBContext.Products.Add(product);
            dBContext.OrderItems.Add(orderItem);
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();
            Order order = dBContext.Orders.FirstOrDefault();

            OrderViewModel actualEntity = await orderService.OrderDetails(order.Id);

            Assert.AreEqual(order.ClientId, actualEntity.ClientId, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.ClientId)));
        }
        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectClientName()
        {
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();

            OrderViewModel actualEntity = await orderService.OrderDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.ClientName, actualEntity.ClientName, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.ClientName)));
        }
        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectProductsCount()
        {
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();

            OrderViewModel actualEntity = await orderService.OrderDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Products.Count, actualEntity.Products.Count, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.Products)));
        }
        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectProducts()
        {
            Product product = new Product() { Id = "ProductId", Title = "Product", Price = 5, RetailerId = "RetailerId" };
            OrderItem orderItem = new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 };
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { }
            };
            dBContext.Products.Add(product);
            dBContext.OrderItems.Add(orderItem);
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();
            Order order = dBContext.Orders.FirstOrDefault();

            OrderViewModel actualEntity = await orderService.OrderDetails(expectedEntity.Id);

            for (int i = 0; i < actualEntity.Products.Count(); i++)
            {
            Assert.AreEqual(expectedEntity.Products[i].ProductId, actualEntity.Products[i].Id, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.Products)));
            }
        }
        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectIsCompleted()
        {
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();

            OrderViewModel actualEntity = await orderService.OrderDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.IsCompleted, actualEntity.IsCompleted, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.IsCompleted)));
        }
        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectRetailerId()
        {
            Product product = new Product() { Id = "ProductId", Title = "Product", Price = 5, RetailerId = "RetailerId" };
            OrderItem orderItem = new OrderItem() { Id = "OrderItemId", ProductId = "ProductId", Quantity = 1 };
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = product.RetailerId,
                IsCompleted = false,
                RetailerId = product.RetailerId,
                TotalPrice = 5,
                Products = new List<OrderItem>() { orderItem }
            };
            dBContext.Products.Add(product);
            dBContext.OrderItems.Add(orderItem);
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();
            Order order = dBContext.Orders.FirstOrDefault();
            OrderViewModel actualEntity = await orderService.OrderDetails(order.Id);

            Assert.AreEqual(order.RetailerId, actualEntity.RetailerId, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.RetailerId)));
        }

        [Test]
        public async Task OrderDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectTotalPrice()
        {
            Order expectedEntity = new Order()
            {
                Id = "OrderId",
                Adress = "Adress",
                ClientId = "ClientId",
                ClientName = "ClientName",
                IsCompleted = false,
                RetailerId = "RetailerId",
                TotalPrice = 5,
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemID", ProductId = "ProductId", Quantity = 1 } }
            };
            dBContext.Orders.Add(expectedEntity);
            dBContext.SaveChanges();

            OrderViewModel actualEntity = await orderService.OrderDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.TotalPrice, actualEntity.TotalPrice, TestsMessages.MappingErrorMessage(nameof(orderService.OrderDetails), nameof(Order.TotalPrice)));
        }


        [TearDown]
        public void Dispose()
        {
            this.dBContext.Dispose();
            this.orderService = null;
        }

    }
}
