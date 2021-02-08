using AutoMapperConfiguration;
using Microsoft.AspNetCore.Http;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NicheMarket.Data.Models
{
    public class Product : IMapTo<ProductBindingModel>, IMapTo<ProductViewModel>
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }

        public string ImageURL { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public string RetailerId { get; set; }

    }
}
