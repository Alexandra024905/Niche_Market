using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Web.Models.ViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string RetailerId { get; set; }
        public string Adress { get; set; }
        public string ClientName { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductViewModel> Products { get; set; } 
        public bool IsCompleted { get; set; }


    }
}
