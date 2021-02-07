using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Web.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
