using Microsoft.AspNetCore.Http;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Web.Models.BindingModels
{
    public class CreateProductBindingModel
    {
        //public string Title { get; set; }
        //public IFormFile FileUpload { get; set; }
        //public string Type { get; set; }

        //public string Description { get; set; }


        //public decimal Price { get; set; }

        //public string RetailerId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = ErrorMessages.TooBigStringTitle)]
        public string Title { get; set; }

        public IFormFile FileUpload { get; set; }

        [MaxLength(200, ErrorMessage = ErrorMessages.TooBigStringType)]
        public string Type { get; set; }

        [MaxLength(200, ErrorMessage = ErrorMessages.TooBigStringDescription)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string RetailerId { get; set; }
    }
}
