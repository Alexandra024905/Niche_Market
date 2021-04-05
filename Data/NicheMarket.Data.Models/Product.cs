using AutoMapperConfiguration;
using Microsoft.AspNetCore.Http;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NicheMarket.Data.Models
{
    public class Product : IMapTo<ProductBindingModel>, IMapTo<ProductViewModel>, IMapFrom<ProductViewModel>
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }

        public string ImageURL { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(28, 20)")]
        public decimal Price { get; set; }
        public string RetailerId { get; set; }

    }
}
