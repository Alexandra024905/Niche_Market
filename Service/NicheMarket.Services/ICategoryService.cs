using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryViewModel>> AllCategories();
        public Task<bool> CreateCategory(CreateCategoryModel newCategory);

        public Task<bool> DeleteCategory(string id);

        public Task<bool> EditCategory(CategoryViewModel newCategory);

        public Task<CategoryViewModel> FindCategory(string id);
    }
}
