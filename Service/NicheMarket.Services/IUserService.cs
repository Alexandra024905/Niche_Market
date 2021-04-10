using NicheMarket.Data.Models.Users;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserRoleViewModel>> AllUsers();
        Task<bool> EditUserRole(UserRoleViewModel userRoleViewModel);
        Task<bool> DeleteUser(string id);
        Task<UserRoleViewModel> FindUserRole(string userId, string roleId);

        Task<NicheMarketUser> EditProfil(UserBindingModel userBindingModel, NicheMarketUser user);
        Task<UserBindingModel> ProfileDetails(NicheMarketUser user);
        Task<UserBindingModel> ChangeRole (NicheMarketUser user);
    }
}
