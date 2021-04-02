using AutoMapperConfiguration;
using NicheMarket.Data.Models;
using NicheMarket.Web.Models.BindingModels;
using System;
using System.Collections.Generic;

namespace NicheMarket.Services.Models
{
    public class ProductServiceModel : IMapTo<Product>, IMapTo<ProductBindingModel>, IMapFrom<CreateProductBindingModel>, IMapTo<ProductServiceModel>, IMapFrom<ProductBindingModel>
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string ImageURL { get; set; }

        public List<string> Type { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public string RetailerId { get; set; }
    }
}
