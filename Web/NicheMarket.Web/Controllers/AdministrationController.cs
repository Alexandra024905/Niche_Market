using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NicheMarket.Services;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{

    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
       
        private readonly IProductService productService;
        private readonly IUserService userService;
      

        public AdministrationController(RoleManager<IdentityRole> roleManager, IProductService productService, IUserService userService)
        {
            this.roleManager = roleManager;
            this.productService = productService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }       

        //add Delete,Edit role
        [HttpPost("/Administration/CreateRole")]
        public async Task<IActionResult> CreateRole (CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRoleViewModel.RoleName
                };
                IdentityResult identityResult = await this.roleManager.CreateAsync(identityRole);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach(IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(createRoleViewModel);
        }

        public async Task<IActionResult> Products()
        {
            return View( await productService.AllProducts());
        }


        public async Task<IActionResult> Users()
        {
            return View(await userService.AllUsers());
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRole(string userId, string roleId)
        {
            return View(await userService.FindUserRole(userId, roleId));
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(UserRoleViewModel userRoleViewModel)
        {
           await userService.EditUserRole(userRoleViewModel);
            return Redirect ("/Administration/Users");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await userService.DeleteUser(userId);
            return Redirect("/Administration/Users");
        }
    }
}
