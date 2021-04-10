using AutoMapperConfiguration;
using Microsoft.EntityFrameworkCore;
using Moq;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Services.Models;
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
    class RetailerServiceTests
    {
        private NicheMarketDBContext dBContext;
        private IRetailerService retailerService;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.RegisterMappings(
    typeof(Product).Assembly.GetTypes(),
    typeof(ProductViewModel).Assembly.GetTypes(),
    typeof(Order).Assembly.GetTypes(),
    typeof(OrderViewModel).Assembly.GetTypes()
    );


            DbContextOptions<NicheMarketDBContext> options = new DbContextOptionsBuilder<NicheMarketDBContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid()}")
                .Options;

            this.dBContext = new NicheMarketDBContext(options);
            this.retailerService = new RetailerService(dBContext);
        }

        [Test]
        public async Task MyProducts_ValidData_ShouldReturnListOfProductViewModels()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            Assert.IsNotEmpty(await retailerService.MyProducts(retailerId), TestsMessages.ResultErrorMessage(nameof(retailerService.MyProducts)));
            Assert.IsNotNull(await retailerService.MyProducts(retailerId), TestsMessages.ResultErrorMessage(nameof(retailerService.MyProducts)));
        }

        [Test]
        public async Task MyProducts_ValidData_ShouldReturnTruthyValue()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await retailerService.MyProducts(retailerId)).ToList();

            Assert.AreEqual(actualProducts.Count, expectedProducts.Count, TestsMessages.ResultErrorMessage(nameof(retailerService.MyProducts)));
        }

        [Test]
        public async Task MyProducts_ValidData_ShouldReturnEntitiesWithEqualIdValues()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await retailerService.MyProducts(retailerId)).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Id, expectedProducts[i].Id, TestsMessages.MappingErrorMessage(nameof(retailerService.MyProducts), nameof(Product.Id)));
            }

        }


        [Test]
        public async Task MyProducts_ValidData_ShouldReturnEntitiesWithEqualTitleValues()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await retailerService.MyProducts(retailerId)).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Title, expectedProducts[i].Title, TestsMessages.MappingErrorMessage(nameof(retailerService.MyProducts), nameof(Product.Title)));
            }

        }

        [Test]
        public async Task MyProducts_ValidData_ShouldReturnEntitiesWithEqualPriceValues()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await retailerService.MyProducts(retailerId)).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Price, expectedProducts[i].Price, TestsMessages.MappingErrorMessage(nameof(retailerService.MyProducts), nameof(Product.Price)));
            }

        }

        [Test]
        public async Task MyProducts_ValidData_ShouldReturnEntitiesWithEqualDescriptionValues()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await retailerService.MyProducts(retailerId)).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Description, expectedProducts[i].Description, TestsMessages.MappingErrorMessage(nameof(retailerService.MyProducts), nameof(Product.Description)));
            }

        }

        [Test]
        public async Task MyProducts_ValidData_ShouldReturnEntitiesWithEqualImageURLValues()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await retailerService.MyProducts(retailerId)).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].ImageURL, expectedProducts[i].ImageURL, TestsMessages.MappingErrorMessage(nameof(retailerService.MyProducts), nameof(Product.ImageURL)));
            }

        }

        [Test]
        public async Task MyProducts_ValidData_ShouldReturnEntitiesWithEqualRetailerIdValues()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await retailerService.MyProducts(retailerId)).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].RetailerId, expectedProducts[i].RetailerId, TestsMessages.MappingErrorMessage(nameof(retailerService.MyProducts), nameof(Product.RetailerId)));
            }

        }

        [Test]
        public async Task MyProducts_ValidData_ShouldReturnEntitiesWithEqualTypeValues()
        {
            string retailerId = "69320701-412e-4de3-8e43-b39b17439c73";
            Product product = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                RetailerId = retailerId
            };
            for (int i = 0; i < 3; i++)
            {
                product.Id = Guid.NewGuid().ToString();
                dBContext.Products.Add(product);
            }
            dBContext.SaveChanges();

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await retailerService.MyProducts(retailerId)).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Type, expectedProducts[i].Type, TestsMessages.MappingErrorMessage(nameof(retailerService.MyProducts), nameof(Product.Type)));
            }

        }



        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnListOfProductViewModels()
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

            Assert.IsNotEmpty(await retailerService.PendingOrders(userId), TestsMessages.ResultErrorMessage(nameof(retailerService.PendingOrders)));
            Assert.IsNotNull(await retailerService.PendingOrders(userId), TestsMessages.ResultErrorMessage(nameof(retailerService.PendingOrders)));
        }

        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnTruthyValue()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            Assert.AreEqual(actualOrderss.Count, expectedOrders.Count, TestsMessages.ResultErrorMessage(nameof(retailerService.PendingOrders)));
        }

        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualIdValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Id, expectedOrders[i].Id, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.Id)));
            }

        }

        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualRetailerIdValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].RetailerId, expectedOrders[i].RetailerId, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.RetailerId)));
            }

        }

        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualClientIdValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].ClientId, expectedOrders[i].ClientId, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.ClientId)));
            }
        }


        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualClientNameValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].ClientName, expectedOrders[i].ClientName, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.ClientName)));
            }
        }


        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualAdressValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Adress, expectedOrders[i].Adress, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.Adress)));
            }
        }


        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualIsCompletedValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].IsCompleted, expectedOrders[i].IsCompleted, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.IsCompleted)));
            }
        }
        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualTotalPriceValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].TotalPrice, expectedOrders[i].TotalPrice, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.TotalPrice)));
            }
        }


        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualProductsCount()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Products.Count, expectedOrders[i].Products.Count, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.Products)));
            }
        }


        [Test]
        public async Task PendingOrders_ValidData_ShouldReturnEntitiesWithEqualProductsValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == false).ToList();

            List<OrderViewModel> actualOrders = (await retailerService.PendingOrders(userId)).ToList();

            for (int i = 0; i < actualOrders.Count; i++)
            {
                for (int j = 0; j < actualOrders[i].Products.Count; j++)
                {
                    Assert.AreEqual(actualOrders[i].Products[j].Id, expectedOrders[i].Products[j].ProductId, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.Products)));
                }
            }

        }




        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnListOfProductViewModels()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            Assert.IsNotEmpty(await retailerService.CompletedOrders(userId), TestsMessages.ResultErrorMessage(nameof(retailerService.CompletedOrders)));
            Assert.IsNotNull(await retailerService.CompletedOrders(userId), TestsMessages.ResultErrorMessage(nameof(retailerService.CompletedOrders)));
        }

        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnTruthyValue()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            Assert.AreEqual(actualOrderss.Count, expectedOrders.Count, TestsMessages.ResultErrorMessage(nameof(retailerService.CompletedOrders)));
        }

        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualIdValues()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Id, expectedOrders[i].Id, TestsMessages.MappingErrorMessage(nameof(retailerService.CompletedOrders), nameof(Order.Id)));
            }

        }

        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualRetailerIdValues()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].RetailerId, expectedOrders[i].RetailerId, TestsMessages.MappingErrorMessage(nameof(retailerService.CompletedOrders), nameof(Order.RetailerId)));
            }

        }

        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualClientIdValues()
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

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].ClientId, expectedOrders[i].ClientId, TestsMessages.MappingErrorMessage(nameof(retailerService.CompletedOrders), nameof(Order.ClientId)));
            }
        }


        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualClientNameValues()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].ClientName, expectedOrders[i].ClientName, TestsMessages.MappingErrorMessage(nameof(retailerService.CompletedOrders), nameof(Order.ClientName)));
            }
        }


        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualAdressValues()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Adress, expectedOrders[i].Adress, TestsMessages.MappingErrorMessage(nameof(retailerService.CompletedOrders), nameof(Order.Adress)));
            }
        }


        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualIsCompletedValues()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].IsCompleted, expectedOrders[i].IsCompleted, TestsMessages.MappingErrorMessage(nameof(retailerService.CompletedOrders), nameof(Order.IsCompleted)));
            }
        }
        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualTotalPriceValues()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].TotalPrice, expectedOrders[i].TotalPrice, TestsMessages.MappingErrorMessage(nameof(retailerService.PendingOrders), nameof(Order.TotalPrice)));
            }
        }


        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualProductsCount()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrderss = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrderss.Count; i++)
            {
                Assert.AreEqual(actualOrderss[i].Products.Count, expectedOrders[i].Products.Count, TestsMessages.MappingErrorMessage(nameof(retailerService.CompletedOrders), nameof(Order.Products)));
            }
        }


        [Test]
        public async Task CompletedOrders_ValidData_ShouldReturnEntitiesWithEqualProductsValues()
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
                IsCompleted = true,
                TotalPrice = mockProduct.Object.Price,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            order.Id = "2";
            dBContext.Orders.Add(order);
            order.Id = "3";
            dBContext.Orders.Add(order);
            dBContext.SaveChanges();

            List<Order> expectedOrders = dBContext.Orders.Where(o => o.IsCompleted == true).ToList();

            List<OrderViewModel> actualOrders = (await retailerService.CompletedOrders(userId)).ToList();

            for (int i = 0; i < actualOrders.Count; i++)
            {
                for (int j = 0; j < actualOrders[i].Products.Count; j++)
                {
                    Assert.AreEqual(actualOrders[i].Products[j].Id, expectedOrders[i].Products[j].ProductId, TestsMessages.MappingErrorMessage(nameof(retailerService.CompletedOrders), nameof(Order.Products)));
                }
            }

        }


        [Test]
        public async Task CompleteOrder_ValidData_ShouldReturnTruthyValue()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemId", ProductId = "ProductId", Quantity = 1 } },
                IsCompleted = false,
                TotalPrice = 5,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            dBContext.SaveChanges();
            bool result = await retailerService.ComleteOrder(order.Id);
            Assert.True(result, TestsMessages.ResultErrorMessage(nameof(retailerService.ComleteOrder)));
        }

        [Test]
        public async Task CompleteOrder_ValidData_ShouldCorrectlyChangeIsCopletedValue()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Order expectedOrder = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemId", ProductId = "ProductId", Quantity = 1 } },
                IsCompleted = false,
                TotalPrice = 5,
                RetailerId = userId
            };

            dBContext.Orders.Add(expectedOrder);
            dBContext.SaveChanges();
            await retailerService.ComleteOrder(expectedOrder.Id);
            Order actualOrder = dBContext.Orders.Find(expectedOrder.Id);
            Assert.AreEqual(expectedOrder.IsCompleted,actualOrder.IsCompleted, TestsMessages.ResultErrorMessage(nameof(retailerService.ComleteOrder)));
        }      
        
        [Test]
        public async Task CompleteOrder_InvalidData_ShouldReturnFalse()
        {
            bool result = await retailerService.ComleteOrder("non-existent order id");
            Assert.False(result, TestsMessages.ReturnsTrueWhenFalseIsExpected(nameof(retailerService.ComleteOrder)));
        }

        [Test]
        public async Task UndoOrder_ValidData_ShouldReturnTruthyValue()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Order order = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemId", ProductId = "ProductId", Quantity = 1 } },
                IsCompleted = true,
                TotalPrice = 5,
                RetailerId = userId
            };

            dBContext.Orders.Add(order);
            dBContext.SaveChanges();
            bool result = await retailerService.UndoOrder(order.Id);
            Assert.True(result, TestsMessages.ResultErrorMessage(nameof(retailerService.UndoOrder)));
        }

        [Test]
        public async Task UndoOrder_ValidData_ShouldCorrectlyChangeIsCopletedValue()
        {
            string userId = "69320701-412e-4de3-8e43-b39b17439c73";
            Order expectedOrder = new Order()
            {
                Id = "1",
                ClientId = userId,
                ClientName = "Client",
                Adress = "Adress",
                Products = new List<OrderItem>() { new OrderItem() { Id = "OrderItemId", ProductId = "ProductId", Quantity = 1 } },
                IsCompleted = true,
                TotalPrice = 5,
                RetailerId = userId
            };

            dBContext.Orders.Add(expectedOrder);
            dBContext.SaveChanges();
            await retailerService.UndoOrder(expectedOrder.Id);
            Order actualOrder = dBContext.Orders.Find(expectedOrder.Id);
            Assert.AreEqual(expectedOrder.IsCompleted, actualOrder.IsCompleted, TestsMessages.ResultErrorMessage(nameof(retailerService.UndoOrder)));
        }

        [Test]
        public async Task UndoOrder_InvalidData_ShouldReturnFalse()
        {
            bool result = await retailerService.UndoOrder("non-existent order id");
            Assert.False(result, TestsMessages.ReturnsTrueWhenFalseIsExpected(nameof(retailerService.UndoOrder)));
        }

        [TearDown]
        public void Dispose()
        {
            this.dBContext.Dispose();
            this.retailerService = null;
        }
    }
}
