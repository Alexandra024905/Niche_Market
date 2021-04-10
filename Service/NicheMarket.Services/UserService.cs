using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NicheMarket.Data;
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
            return userRoleViewModel;

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


        public async Task<UserBindingModel> ProfileDetails(NicheMarketUser user)
        {
            UserBindingModel userBindingModel = new UserBindingModel()
            {
                UserId = user.Id,
                RoleId = (await dBContext.UserRoles.Where(ur => ur.UserId == user.Id).FirstOrDefaultAsync()).RoleId,
                Email = user.Email,
                Adress = user.Address,
                Name = user.Name
            };

            return userBindingModel;
        }

        public async Task<UserBindingModel> ChangeRole(NicheMarketUser user)
        {
            UserBindingModel userBindingModel = new UserBindingModel()
            {
                UserId = user.Id,
                RoleId = (await dBContext.UserRoles.Where(ur => ur.UserId == user.Id).FirstOrDefaultAsync()).RoleId,
                Email = user.Email,
                Adress = user.Address,
                Name = user.Name
            };

            string roleName = FindRoleName(userBindingModel.RoleId);
            if (roleName == "Admin")
            {
                await userManager.AddToRoleAsync(user, "Client");
                await userManager.RemoveFromRoleAsync(user, roleName);
            }
            else
            {
                await userManager.AddToRoleAsync(user, "Admin");
                await userManager.RemoveFromRoleAsync(user, roleName);
            }

            return userBindingModel;
        }
        public async Task<NicheMarketUser> EditProfil(UserBindingModel userBindingModel, NicheMarketUser user)
        {
            if (userBindingModel.NewPassword != null)
            {
                user.Address = userBindingModel.Adress;
                user.Email = userBindingModel.Email;
                user.Name = userBindingModel.Name;
            }

            dBContext.Users.Update(user);
            await dBContext.SaveChangesAsync();

            return user;
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
