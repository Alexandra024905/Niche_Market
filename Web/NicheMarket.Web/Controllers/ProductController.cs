﻿using AutoMapperConfiguration;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NicheMarket.Data.Models;
using NicheMarket.Services;
using NicheMarket.Services.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NicheMarket.Web.Controllers
{

    public class ProductController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public ProductController(ICloudinaryService cloudinaryService, IProductService productService, ICategoryService categoryService)
        {
            this.cloudinaryService = cloudinaryService;
            this.productService = productService;
            this.categoryService = categoryService;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await productService.AllProducts());
        }

        [HttpGet]
        [Route("Product/Create/{id?}")]
        public IActionResult Create()
        {
            ViewData["Categories"] = categoryService.AllCategories();
            ViewData["Product"] = new CreateProductBindingModel();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Retailer")]
        public async Task<IActionResult> Create(CreateProductBindingModel createProductBindingModel)
        {
            ProductServiceModel productServiceModel = createProductBindingModel.To<ProductServiceModel>();
            productServiceModel.RetailerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (createProductBindingModel.FileUpload != null)
            {
                string url = await this.cloudinaryService.UploadImage(createProductBindingModel.FileUpload);
                productServiceModel.ImageURL = url;
            }
            await productService.CreateProduct(productServiceModel);

            return Redirect("/Retailer/RetailerProducts");
        }


        [HttpGet]
        [Authorize(Roles = "Retailer")]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await productService.GetProduct(id));
        }

        [HttpPost]
        [Authorize(Roles = "Retailer")]
        public async Task<IActionResult> Edit(ProductBindingModel product)
        {
            ProductServiceModel serviceModel = product.To<ProductServiceModel>();
            if (product.Image != null)
            {
                string url = await this.cloudinaryService.UploadImage(product.Image);
                serviceModel.ImageURL = url;
            }
            await productService.EditProduct(serviceModel);

            return Redirect("/Retailer/RetailerProducts");
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            ProductViewModel product = await productService.ProductDetails(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpGet]
        [Authorize(Roles = "Retailer,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            return View(await productService.ProductDetails(id));
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Retailer")]
        public async Task<IActionResult> DeleteProductAsAdmin(string id)
        {
            await productService.DeleteProduct(id);
            if (User.IsInRole("Admin"))
            {
            return Redirect("/Administration/Products");
            }
            else 
            {
            return Redirect("/Retailer/RetailerProducts");
            }
        }
    }

}

