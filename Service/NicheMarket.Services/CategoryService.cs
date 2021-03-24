using AutoMapperConfiguration;
using Microsoft.EntityFrameworkCore;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NicheMarketDBContext dBContext;

        public CategoryService(NicheMarketDBContext dBContext)
        {
            this.dBContext = dBContext;
        }


        public async Task<IEnumerable<CategoryViewModel>> AllCategories()
        {
            IEnumerable<CategoryViewModel> categories = await dBContext.Category.Select(c=> c.To<CategoryViewModel>()).ToArrayAsync();
            return  categories;
        }
        public async Task<bool> CreateCategory(CreateCategoryModel newCategory)
        {
            Category category = newCategory.To<Category>();
            category.Id = Guid.NewGuid().ToString();
            bool result = await dBContext.AddAsync(category) != null;
            await dBContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteCategory(string id)
        {
            Category category = await dBContext.Category.FindAsync(id);
            bool result = dBContext.Category.Remove(category) !=null;
            await dBContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> EditCategory(CategoryViewModel newCategory)
        {
            Category category = await dBContext.Category.FindAsync(newCategory.Id);
            category.Name = newCategory.Name;
            bool result =  dBContext.Category.Update(category) != null;
            await dBContext.SaveChangesAsync();
            return  result;
        }

        public async Task<CategoryViewModel> FindCategory(string id)
        {
            return (await dBContext.Category.FindAsync(id)).To<CategoryViewModel>();
        }
    }
}
