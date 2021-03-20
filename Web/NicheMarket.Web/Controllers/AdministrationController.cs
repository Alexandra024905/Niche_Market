using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NicheMarket.Services;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;

        public AdministrationController(RoleManager<IdentityRole> roleManager, IProductService productService, IUserService userService, ICategoryService categoryService)
        {
            this.roleManager = roleManager;
            this.productService = productService;
            this.userService = userService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost("/Administration/CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
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

                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(createRoleViewModel);
        }

        public async Task<IActionResult> Products()
        {
            return View(await productService.AllProducts());
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
            return Redirect("/Administration/Users");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await userService.DeleteUser(userId);
            return Redirect("/Administration/Users");
        }

        public async Task<IActionResult> Categories()
        {
            return View(await categoryService.AllCategories());
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel newCategory)
        {
            await categoryService.CreateCategory(newCategory);
            return Redirect("/Administration/Categories");
        }

        [HttpGet]
        public async  Task<IActionResult> EditCategory(string id)
        {
            return View(await  categoryService.FindCategory(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CategoryViewModel newCategory)
        {
            await categoryService.EditCategory(newCategory);
            return Redirect("/Administration/Categories");
        }
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await categoryService.DeleteCategory(id);
            return Redirect("/Administration/Categories");
        }
    }
}
