using AutoMapperConfiguration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NicheMarket.Data;
using NicheMarket.Data.Models;
using NicheMarket.Data.Models.Users;
using NicheMarket.Services;
using NicheMarket.Services.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NicheMarket.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<ICloudinaryService>(instance => new CloudinaryService(
            this.Configuration["Cloudinary:CloudName"],
            this.Configuration["Cloudinary:ApiKey"],
            this.Configuration["Cloudinary:ApiSecret"]));


            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRetailerService, RetailerService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMvc()
    .AddSessionStateTempDataProvider();
            services.AddSession();

            //services.AddMvc(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //}).AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(CreateProductBindingModel).Assembly.GetTypes(),
                typeof(ProductBindingModel).Assembly.GetTypes(),
                typeof(ProductViewModel).Assembly.GetTypes(),
                typeof(ProductServiceModel).Assembly.GetTypes(),
                typeof(Product).Assembly.GetTypes(),
                typeof(Order).Assembly.GetTypes(),
                typeof(CreateOrderBindingModel).Assembly.GetTypes(),
                typeof(OrderServiceModel).Assembly.GetTypes(),
                typeof(OrderViewModel).Assembly.GetTypes());



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<NicheMarketDBContext>())
                {
                    dbContext.Database.Migrate();

                    if (dbContext.Roles.Count() == 0)
                    {
                        dbContext.Roles.Add(new IdentityRole
                        {
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });
                        dbContext.Roles.Add(new IdentityRole
                        {
                            Name = "Retailer",
                            NormalizedName = "RETAILER",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });

                        dbContext.Roles.Add(new IdentityRole
                        {
                            Name = "Client",
                            NormalizedName = "CLIENT",
                            ConcurrencyStamp = Guid.NewGuid().ToString()
                        });

                        dbContext.SaveChanges();
                    }
                }


                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseSession();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapRazorPages();
                });
            }
        }
    }
}
