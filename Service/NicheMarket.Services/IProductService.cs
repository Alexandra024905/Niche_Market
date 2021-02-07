using NicheMarket.Services.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductServiceModel productServiceModel);
        Task <ProductViewModel> DetailsProduct(string id);
        Task<bool> EditProduct(ProductServiceModel productServiceModel);
        Task<bool> DeleteProduct(string id);
        Task <List<ProductViewModel>> AllProducts();
        Task<ProductBindingModel> GetProduct(string id);
    }
}
