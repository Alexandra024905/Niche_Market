using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NicheMarket.Data;
using NicheMarket.Data.Models.Users;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services
{
    public class UserService : IUserService
    {
        private readonly NicheMarketDBContext dBContext;
        private readonly UserManager<NicheMarketUser> userManager;

        public UserService(NicheMarketDBContext dBContext, UserManager<NicheMarketUser> userManager)
        {
            this.dBContext = dBContext;
            this.userManager = userManager;
        }



        public async Task<IEnumerable<UserRoleViewModel>> AllUsers()
        {
            List<UserRoleViewModel> users = new List<UserRoleViewModel>();

            foreach (var item in dBContext.UserRoles)
            {
                users.Add(new UserRoleViewModel
                {
                    RoleId = item.RoleId,
                    UserId = item.UserId,
                    UserName = (await dBContext.Users.FindAsync(item.UserId)).UserName,
                    RoleName = FindRoleName(item.RoleId)
                });
            }
            return users;
        }

        //to do  EditUserRole(UserRoleSeModel userRoleServiceModel)
        public async Task<bool> EditUserRole(UserRoleViewModel userRoleViewModel)
        {
            bool result = false;
            if (userRoleViewModel.UserId != null && userRoleViewModel.RoleName != null)
            {
                if (RoleExists(userRoleViewModel.RoleName))
                {
                    string oldRoleName = FindRoleName(userRoleViewModel.RoleId);
                    if (oldRoleName != userRoleViewModel.RoleName)
                    {
                        NicheMarketUser user = FindUser(userRoleViewModel.UserId);
                        await userManager.AddToRoleAsync(user, userRoleViewModel.RoleName);
                        await userManager.RemoveFromRoleAsync(user, oldRoleName);
                        dBContext.SaveChanges();
                    }
                    result = true;
                }
            }
            return result;
        }

        public async Task<UserRoleViewModel> FindUserRole(string userId, string roleId)
        {
            UserRoleViewModel userRoleViewModel = new UserRoleViewModel
            {
                RoleId = roleId,
                UserId = userId,
                UserName = (await dBContext.Users.FindAsync(userId)).UserName,
                RoleName = FindRoleName(roleId)
            };
            return  userRoleViewModel;

        }

        public async Task<bool> DeleteUser(string id)
        {
            bool result = false;
            if (id != null)
            {
                if (UserExists(id))
                {
                    NicheMarketUser user = await dBContext.Users.FindAsync(id);
                    dBContext.Users.Remove(user);
                    dBContext.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        private bool UserExists(string id)
        {
            return dBContext.Users.Any(e => e.Id == id);
        }

        private NicheMarketUser FindUser(string id)
        {
            return dBContext.Users.FirstOrDefault(u => u.Id == id);
        }

        private string FindRoleName(string id)
        {
            return dBContext.Roles.FirstOrDefault(r => r.Id == id).Name;
        }
        private string FindRoleId(string roleName)
        {
            return dBContext.Roles.FirstOrDefault(r => r.Name == roleName).Id;
        }

        private bool RoleExists(string roleName)
        {
            return dBContext.Roles.Any(r => r.Name == roleName);
        }

    }
}
