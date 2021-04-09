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
    class CategoryServiceTests
    {
        private NicheMarketDBContext dBContext;
        private ICategoryService categoryService;

        [SetUp]
        public void SetUp()
        {
            AutoMapperConfig.RegisterMappings(
    typeof(Category).Assembly.GetTypes(),
    typeof(CreateCategoryModel).Assembly.GetTypes(),
    typeof(CategoryViewModel).Assembly.GetTypes()
    );


            DbContextOptions<NicheMarketDBContext> options = new DbContextOptionsBuilder<NicheMarketDBContext>()
                .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid()}")
                .Options;

            this.dBContext = new NicheMarketDBContext(options);
            this.categoryService = new CategoryService(dBContext);
        }


        [Test]
        public async Task CreateCategory_ValidData_ShouldReturnTruthyValue()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            bool result = await categoryService.CreateCategory(createCategoryModel);
            Assert.True(result, TestsMessages.ResultErrorMessage(nameof(categoryService.CreateCategory)));
        }

        [Test]
        public async Task CreateCategory_ValidData_ShouldCorrectlyCreateEntity()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            await categoryService.CreateCategory(createCategoryModel);
            Category actualEntity = await dBContext.Category.FirstOrDefaultAsync();

            Assert.IsNotNull(actualEntity, TestsMessages.InvalidValueErrorMessage(nameof(categoryService.CreateCategory)));
        }

        [Test]
        public async Task CreateCategoryWithName_ValidData_ShouldCorrectlyMapData()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            Category expectedEntity = new Category()
            {
                Name = "Test"
            };

            await categoryService.CreateCategory(createCategoryModel);
            Category actualEntity = await dBContext.Category.FirstOrDefaultAsync();

            Assert.AreEqual(expectedEntity.Name, actualEntity.Name, TestsMessages.MappingErrorMessage(nameof(categoryService.CreateCategory), nameof(actualEntity.Name)));
        }

        [Test]
        public async Task EditCategory_ValidData_ShouldReturnTrue()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            await categoryService.CreateCategory(createCategoryModel);
            Category expectedEntity = await dBContext.Category.FirstOrDefaultAsync();

            CategoryViewModel categoryViewModel = new CategoryViewModel { Id = expectedEntity.Id, Name = "New name" };
            bool result = await categoryService.EditCategory(categoryViewModel);

            Assert.IsTrue(result, TestsMessages.ResultErrorMessage(nameof(categoryService.EditCategory)));
        }


        [Test]
        public async Task EditCategory_ValidData_ShouldCorrectlyEditEntity()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            await categoryService.CreateCategory(createCategoryModel);
            Category expectedEntity = await dBContext.Category.FirstOrDefaultAsync();

            CategoryViewModel categoryViewModel = new CategoryViewModel { Id = expectedEntity.Id, Name = "New name" };
            await categoryService.EditCategory(categoryViewModel);
            Category actualEntity = await dBContext.Category.FirstOrDefaultAsync();

            Assert.AreEqual(expectedEntity.Name, actualEntity.Name, TestsMessages.MappingErrorMessage(nameof(categoryService.EditCategory), nameof(actualEntity.Name)));
        }


        [Test]
        public async Task EditCategory_ValidData_ShouldBeDifferentFromTheOldEntity()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            await categoryService.CreateCategory(createCategoryModel);
            Category oldEntity = await dBContext.Category.FirstOrDefaultAsync();

            CategoryViewModel categoryViewModel = new CategoryViewModel { Id = oldEntity.Id, Name = "New name" };
            await categoryService.EditCategory(categoryViewModel);
            Category actualEntity = await dBContext.Category.FirstOrDefaultAsync();

            Assert.AreNotEqual(createCategoryModel.Name, actualEntity.Name, TestsMessages.MappingErrorMessage(nameof(categoryService.EditCategory), nameof(actualEntity.Name)));
        }

        [Test]
        public async Task AllCategories_ValidData_ShouldReturnListOfProductViewModels()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            await categoryService.CreateCategory(createCategoryModel);
            await categoryService.CreateCategory(createCategoryModel);
            await categoryService.CreateCategory(createCategoryModel);

            Assert.IsNotEmpty(await categoryService.AllCategories(), TestsMessages.ResultErrorMessage(nameof(categoryService.AllCategories)));
            Assert.IsNotNull(await categoryService.AllCategories(), TestsMessages.ResultErrorMessage(nameof(categoryService.AllCategories)));
        }

        [Test]
        public async Task AllCategories_ValidData_ShouldReturnTruthyValue()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            await categoryService.CreateCategory(createCategoryModel);
            await categoryService.CreateCategory(createCategoryModel);
            await categoryService.CreateCategory(createCategoryModel);

            List<Category> expectedCategories = dBContext.Category.ToList();

            List<CategoryViewModel> actualCategories = (await categoryService.AllCategories()).ToList();

            Assert.AreEqual(expectedCategories.Count, actualCategories.Count, TestsMessages.ResultErrorMessage(nameof(categoryService.AllCategories)));
        }

        [Test]
        public async Task AllCategories_ValidData_ShouldReturnEntitiesWithEqualIdValues()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            for (int i = 0; i < 3; i++)
            {
                createCategoryModel.Name += i.ToString();
                await categoryService.CreateCategory(createCategoryModel);
            }


            List<Category> expectedCategories = dBContext.Category.ToList();

            List<CategoryViewModel> actualCategories = (await categoryService.AllCategories()).ToList();

            for (int i = 0; i < actualCategories.Count; i++)
            {
                Assert.AreEqual(actualCategories[i].Id, expectedCategories[i].Id, TestsMessages.MappingErrorMessage(nameof(categoryService.AllCategories), nameof(Category.Id)));
            }

        }


        [Test]
        public async Task AllCategories_ValidData_ShouldReturnEntitiesWithEqualNameValues()
        {
            CreateCategoryModel createCategoryModel = new CreateCategoryModel()
            {
                Name = "Test"
            };

            for (int i = 0; i < 3; i++)
            {
                createCategoryModel.Name += i.ToString();
                await categoryService.CreateCategory(createCategoryModel);
            }


            List<Category> expectedCategories = dBContext.Category.ToList();

            List<CategoryViewModel> actualCategories = (await categoryService.AllCategories()).ToList();

            for (int i = 0; i < actualCategories.Count; i++)
            {
                Assert.AreEqual(actualCategories[i].Name, expectedCategories[i].Name, TestsMessages.MappingErrorMessage(nameof(categoryService.AllCategories), nameof(Category.Name)));
            }

        }



        [TearDown]
        public void Dispose()
        {
            this.dBContext.Dispose();
            this.categoryService = null;
        }

    }

}

