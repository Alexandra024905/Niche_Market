using Microsoft.AspNetCore.Http;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Web.Models.BindingModels
{
    public class CreateProductBindingModel
    {
        public string Title { get; set; }
        public IFormFile FileUpload { get; set; }
        public List<string> Type { get; set; }

        public string Description { get; set; }


        public decimal Price { get; set; }

        public string RetailerId { get; set; }
    }
}
