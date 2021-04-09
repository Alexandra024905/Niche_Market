using AutoMapperConfiguration;
using Microsoft.EntityFrameworkCore;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Services.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicheMarket.Services.Tests
{
    [TestFixture]
    class ProductServiceTests
    {
        private NicheMarketDBContext dBContext;
        private IProductService productService;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(Product).Assembly.GetTypes(),
                typeof(ProductServiceModel).Assembly.GetTypes()
                );

            DbContextOptions<NicheMarketDBContext> options = new DbContextOptionsBuilder<NicheMarketDBContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid()}")
                .Options;

            this.dBContext = new NicheMarketDBContext(options);
            this.productService = new ProductService(dBContext);
        }


        [Test]
        public async Task CreateProduct_ValidData_ShouldReturnTruthyValue()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            bool result = await productService.CreateProduct(productServiceModel);
            Assert.True(result, TestsMessages.ResultErrorMessage(nameof(productService.CreateProduct)));
        }

        [Test]
        public async Task CreateProduct_ValidData_ShouldCorrectlyCreateEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product actualEntity = await dBContext.Products.FirstOrDefaultAsync();

            Assert.IsNotNull(actualEntity);
        }

        [Test]
        public async Task CreateProductWithCorrectTitle_ValidData_ShouldCorrectlyMapData()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            Product expectedEntity = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product actualEntity = await dBContext.Products.FirstOrDefaultAsync();
            Assert.AreEqual(expectedEntity.Title, actualEntity.Title, TestsMessages.MappingErrorMessage(nameof(productService.CreateProduct), nameof(actualEntity.Title)));
        }


        [Test]
        public async Task CreateProductWithCorrectPrice_ValidData_ShouldCorrectlyMapData()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            Product expectedEntity = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product actualEntity = await dBContext.Products.FirstOrDefaultAsync();
            Assert.AreEqual(expectedEntity.Price, actualEntity.Price, TestsMessages.MappingErrorMessage(nameof(productService.CreateProduct), nameof(actualEntity.Price)));
        }

        [Test]
        public async Task CreateProductWithCorrectImageUrl_ValidData_ShouldCorrectlyMapData()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            Product expectedEntity = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product actualEntity = await dBContext.Products.FirstOrDefaultAsync();
            Assert.AreEqual(expectedEntity.ImageURL, actualEntity.ImageURL, TestsMessages.MappingErrorMessage(nameof(productService.CreateProduct), nameof(actualEntity.ImageURL)));
        }

        [Test]
        public async Task CreateProductWithCorrectType_ValidData_ShouldCorrectlyMapData()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            Product expectedEntity = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product actualEntity = await dBContext.Products.FirstOrDefaultAsync();
            Assert.AreEqual(expectedEntity.Type, actualEntity.Type, TestsMessages.MappingErrorMessage(nameof(productService.CreateProduct), nameof(actualEntity.Type)));
        }

        [Test]
        public async Task CreateProductWithCorrectDescription_ValidData_ShouldCorrectlyMapData()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            Product expectedEntity = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product actualEntity = await dBContext.Products.FirstOrDefaultAsync();
            Assert.AreEqual(expectedEntity.Description, actualEntity.Description, TestsMessages.MappingErrorMessage(nameof(productService.CreateProduct), nameof(actualEntity.Description)));
        }

        [Test]
        public async Task CreateProductWithCorrectRetailerId_ValidData_ShouldCorrectlyMapData()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            Product expectedEntity = new Product()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product actualEntity = await dBContext.Products.FirstOrDefaultAsync();
            Assert.AreEqual(expectedEntity.RetailerId, actualEntity.RetailerId, TestsMessages.MappingErrorMessage(nameof(productService.CreateProduct), nameof(actualEntity.RetailerId)));
        }

        [Test]
        public async Task CreateProductSetIdCorrectly_ValidData_ShouldCorrectlySetProductId()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product actualEntity = await dBContext.Products.FirstOrDefaultAsync();

            Assert.IsNotNull(actualEntity.Id, TestsMessages.SetPropertyIncorrectlyErrorMessage(nameof(productService.CreateProduct), nameof(Product.Id)));
        }


        [Test]
        public async Task EditProduct_ValidData_ShouldReturnTrue()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            Product entity = await dBContext.Products.FirstOrDefaultAsync();
            productServiceModel.Id = entity.Id;
            bool result = await productService.EditProduct(productServiceModel);
            Assert.IsTrue(result, TestsMessages.ResultErrorMessage(nameof(productService.EditProduct)));
        }

        [Test]
        public async Task EditProductPrice_ValidData_ShouldCorrectlyEditEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.Price = 5;
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreEqual(expectedEntity.Price, actualEntity.Price, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Price)));
        }


        [Test]
        public async Task EditProductPrice_ValidData_ShouldBeDifferentFromTheOldEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.Price = 5;
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreNotEqual(productServiceModel.Price, actualEntity.Price, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Price)));
        }


        [Test]
        public async Task EditProductTitle_ValidData_ShouldCorrectlyEditEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.Title = "New Title";
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreEqual(expectedEntity.Title, actualEntity.Title, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Title)));
        }

        [Test]
        public async Task EditProductTitle_ValidData_ShouldBeDifferentFromTheOldEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.Title = "New Title";
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreNotEqual(productServiceModel.Title, actualEntity.Title, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Title)));
        }


        [Test]
        public async Task EditProductType_ValidData_ShouldCorrectlyEditEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.Type = "New type";
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreEqual(expectedEntity.Type, actualEntity.Type, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Type)));
        }

        [Test]
        public async Task EditProductType_ValidData_ShouldBeDifferentFromTheOldEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.Type = "New type";
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreNotEqual(productServiceModel.Type, actualEntity.Type, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Type)));
        }


        [Test]
        public async Task EditProductDescription_ValidData_ShouldCorrectlyEditEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.Description = "New description";
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreEqual(expectedEntity.Description, actualEntity.Description, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Description)));
        }
        [Test]
        public async Task EditProductDescription_ValidData_ShouldBeDifferentFromTheOldEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.Description = "New description";
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreNotEqual(productServiceModel.Description, actualEntity.Description, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Description)));
        }

        [Test]
        public async Task EditProductImage_ValidData_ShouldCorrectlyEditEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1615983722/d8f9c6d8-a651-4d24-9d4c-987825d8b43d.png";
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreEqual(expectedEntity.ImageURL, actualEntity.ImageURL, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.ImageURL)));
        }

        [Test]
        public async Task EditProductImage_ValidData_ShouldBeDifferentFromTheOldEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            ProductServiceModel expectedEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();
            expectedEntity.ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1615983722/d8f9c6d8-a651-4d24-9d4c-987825d8b43d.png";
            await productService.EditProduct(expectedEntity);
            ProductServiceModel actualEntity = (await dBContext.Products.FirstOrDefaultAsync()).To<ProductServiceModel>();

            Assert.AreNotEqual(productServiceModel.ImageURL, actualEntity.ImageURL, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.ImageURL)));
        }

        [Test]
        public async Task EditProductWithNullId_InvalidData_ShouldReturnFalse()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Id = null,
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);
            bool result = await productService.EditProduct(productServiceModel);
            Assert.IsFalse(result, TestsMessages.ResultErrorMessage(nameof(productService.EditProduct)));
        }

        [Test]
        public async Task AllProducts_ValidData_ShouldReturnListOfProductViewModels()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct1",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            await productService.CreateProduct(productServiceModel);

            productServiceModel.Title = "TestProduct2";
            await productService.CreateProduct(productServiceModel);
            productServiceModel.Title = "TestProduct3";
            await productService.CreateProduct(productServiceModel);

            Assert.IsNotEmpty(await productService.AllProducts(), TestsMessages.ResultErrorMessage(nameof(productService.AllProducts)));
            Assert.IsNotNull(await productService.AllProducts(), TestsMessages.ResultErrorMessage(nameof(productService.AllProducts)));
        }

        [Test]
        public async Task AllProducts_ValidData_ShouldReturnTruthyValue()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct1",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            for (int i = 0; i < 3; i++)
            {
                await productService.CreateProduct(productServiceModel);
            }


            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await productService.AllProducts()).ToList();

            Assert.AreEqual(actualProducts.Count, expectedProducts.Count, TestsMessages.ResultErrorMessage(nameof(productService.AllProducts)));
        }

        [Test]
        public async Task AllProducts_ValidData_ShouldReturnEntitiesWithEqualIdValues()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            for (int i = 0; i < 3; i++)
            {
                await productService.CreateProduct(productServiceModel);
            }

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await productService.AllProducts()).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Id, expectedProducts[i].Id, TestsMessages.MappingErrorMessage(nameof(productService.AllProducts), nameof(Product.Id)));
            }

        }


        [Test]
        public async Task AllProducts_ValidData_ShouldReturnEntitiesWithEqualTitleValues()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            for (int i = 0; i < 3; i++)
            {
                await productService.CreateProduct(productServiceModel);
            }

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await productService.AllProducts()).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Title, expectedProducts[i].Title, TestsMessages.MappingErrorMessage(nameof(productService.AllProducts), nameof(Product.Title)));
            }

        }

        [Test]
        public async Task AllProducts_ValidData_ShouldReturnEntitiesWithEqualPriceValues()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            for (int i = 0; i < 3; i++)
            {
                await productService.CreateProduct(productServiceModel);
            }

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await productService.AllProducts()).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Price, expectedProducts[i].Price, TestsMessages.MappingErrorMessage(nameof(productService.AllProducts), nameof(Product.Price)));
            }

        }

        [Test]
        public async Task AllProducts_ValidData_ShouldReturnEntitiesWithEqualDescriptionValues()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            for (int i = 0; i < 3; i++)
            {
                await productService.CreateProduct(productServiceModel);
            }

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await productService.AllProducts()).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Description, expectedProducts[i].Description, TestsMessages.MappingErrorMessage(nameof(productService.AllProducts), nameof(Product.Description)));
            }

        }

        [Test]
        public async Task AllProducts_ValidData_ShouldReturnEntitiesWithEqualImageURLValues()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            for (int i = 0; i < 3; i++)
            {
                await productService.CreateProduct(productServiceModel);
            }

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await productService.AllProducts()).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].ImageURL, expectedProducts[i].ImageURL, TestsMessages.MappingErrorMessage(nameof(productService.AllProducts), nameof(Product.ImageURL)));
            }

        }

        [Test]
        public async Task AllProducts_ValidData_ShouldReturnEntitiesWithEqualRetailerIdValues()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            for (int i = 0; i < 3; i++)
            {
                await productService.CreateProduct(productServiceModel);
            }

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await productService.AllProducts()).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].RetailerId, expectedProducts[i].RetailerId, TestsMessages.MappingErrorMessage(nameof(productService.AllProducts), nameof(Product.RetailerId)));
            }

        }

        [Test]
        public async Task AllProducts_ValidData_ShouldReturnEntitiesWithEqualTypeValues()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };

            for (int i = 0; i < 3; i++)
            {
                await productService.CreateProduct(productServiceModel);
            }

            List<ProductViewModel> expectedProducts = dBContext.Products.Select(p => p.To<ProductViewModel>()).ToList();

            List<ProductViewModel> actualProducts = (await productService.AllProducts()).ToList();

            for (int i = 0; i < actualProducts.Count; i++)
            {
                Assert.AreEqual(actualProducts[i].Type, expectedProducts[i].Type, TestsMessages.MappingErrorMessage(nameof(productService.AllProducts), nameof(Product.Type)));
            }

        }


        [Test]
        public async Task GetProduct_ValidData_ShouldCorrectlyReturnEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product product = await dBContext.Products.FirstOrDefaultAsync();

            ProductBindingModel productBinding = await productService.GetProduct(product.Id);

            Assert.IsNotNull(productBinding, TestsMessages.ResultErrorMessage(nameof(productService.GetProduct)));
        }

        [Test]
        public async Task GetProduct_ValidData_ShouldCorrectlyReturnEntityWithCorrectTitle()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductBindingModel actualEntity = await productService.GetProduct(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Title, actualEntity.Title, TestsMessages.MappingErrorMessage(nameof(productService.GetProduct), nameof(Product.Title)));
        }

        [Test]
        public async Task GetProduct_ValidData_ShouldCorrectlyReturnEntityWithCorrectDescription()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductBindingModel actualEntity = await productService.GetProduct(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Description, actualEntity.Description, TestsMessages.MappingErrorMessage(nameof(productService.GetProduct), nameof(Product.Description)));
        }

        [Test]
        public async Task GetProduct_ValidData_ShouldCorrectlyReturnEntityWithCorrectPrice()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductBindingModel actualEntity = await productService.GetProduct(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Price, actualEntity.Price, TestsMessages.MappingErrorMessage(nameof(productService.GetProduct), nameof(Product.Price)));
        }


        [Test]
        public async Task GetProduct_ValidData_ShouldCorrectlyReturnEntityWithCorrectRetailerId()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductBindingModel actualEntity = await productService.GetProduct(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.RetailerId, actualEntity.RetailerId, TestsMessages.MappingErrorMessage(nameof(productService.GetProduct), nameof(Product.RetailerId)));
        }


        [Test]
        public async Task GetProduct_ValidData_ShouldCorrectlyReturnEntityWithCorrectType()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductBindingModel actualEntity = await productService.GetProduct(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Type, actualEntity.Type, TestsMessages.MappingErrorMessage(nameof(productService.GetProduct), nameof(Product.Type)));
        }

        [Test]
        public async Task ProductDetails_ValidData_ShouldCorrectlyReturnEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product product = await dBContext.Products.FirstOrDefaultAsync();

            ProductViewModel actualEntity = await productService.ProductDetails(product.Id);

            Assert.IsNotNull(actualEntity, TestsMessages.ResultErrorMessage(nameof(productService.EditProduct)));
        }

        [Test]
        public async Task ProductDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectTitle()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductViewModel actualEntity = await productService.ProductDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Title, actualEntity.Title, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Title)));
        }

        [Test]
        public async Task ProductDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectDescription()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductViewModel actualEntity = await productService.ProductDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Description, actualEntity.Description, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Description)));
        }


        [Test]
        public async Task ProductDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectImageURL()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductViewModel actualEntity = await productService.ProductDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.ImageURL, actualEntity.ImageURL, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.ImageURL)));
        }


        [Test]
        public async Task ProductDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectPrice()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductViewModel actualEntity = await productService.ProductDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Price, actualEntity.Price, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Price)));
        }


        [Test]
        public async Task ProductDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectRetailerId()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductViewModel actualEntity = await productService.ProductDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.RetailerId, actualEntity.RetailerId, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.RetailerId)));
        }


        [Test]
        public async Task ProductDetails_ValidData_ShouldCorrectlyReturnEntityWithCorrectType()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            ProductViewModel actualEntity = await productService.ProductDetails(expectedEntity.Id);

            Assert.AreEqual(expectedEntity.Type, actualEntity.Type, TestsMessages.MappingErrorMessage(nameof(productService.EditProduct), nameof(Product.Type)));
        }

        [Test]
        public async Task DeleteProduct_ValidData_ShouldReturnTrue()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product expectedEntity = await dBContext.Products.FirstOrDefaultAsync();

            bool result = await productService.DeleteProduct(expectedEntity.Id);

            Assert.IsTrue(result, TestsMessages.ResultErrorMessage(nameof(productService.DeleteProduct)));
        }

        [Test]
        public async Task DeleteProduct_ValidData_ShouldCorrectlyDeleteEntity()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product entity = await dBContext.Products.FirstOrDefaultAsync();
            await productService.DeleteProduct(entity.Id);
            List<Product> products = dBContext.Products.ToList();

            Assert.False(products.Contains(entity), TestsMessages.ResultErrorMessage(nameof(productService.EditProduct)));
        }

        [Test]
        public async Task DeleteProductWithNullId_InvalidData_ShouldReturnFalse()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product entity = await dBContext.Products.FirstOrDefaultAsync();
            bool result = await productService.DeleteProduct(null);

            Assert.False(result, TestsMessages.ReturnsTrueWhenFalseIsExpected(nameof(productService.DeleteProduct)));
        }

        [Test]
        public async Task DeleteNonExistentProduct_InvalidData_ShouldReturnFalse()
        {
            ProductServiceModel productServiceModel = new ProductServiceModel()
            {
                Title = "TestProduct",
                Price = 3,
                Type = "TestProduct",
                Description = "TestProduct",
                ImageURL = "http://res.cloudinary.com/niche-market/image/upload/v1612549640/7223595e-3b3e-4452-bd9e-f3fad9130046.png",
                RetailerId = "69320701-412e-4de3-8e43-b39b17439c73"
            };
            await productService.CreateProduct(productServiceModel);

            Product entity = await dBContext.Products.FirstOrDefaultAsync();
            bool result = await productService.DeleteProduct("non-existent id");

            Assert.False(result, TestsMessages.ReturnsTrueWhenFalseIsExpected(nameof(productService.DeleteProduct)));
        }


        [TearDown]
        public void Dispose()
        {
            this.dBContext.Dispose();
            this.productService = null;
        }

    }
}
