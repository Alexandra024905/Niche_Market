using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Web.Models.BindingModels
{
   public class CreateOrderBindingModel
    {
        public string ClientId { get; set; }
        public string RetailerId { get; set; }

        [MaxLength(200, ErrorMessage = ErrorMessages.TooBigStringAdress)]

        public string Adress { get; set; }
        [MaxLength(50, ErrorMessage = ErrorMessages.TooBigStringName)]

        public string ClientName { get; set; }

        public decimal TotalPrice { get; set; }
        public List<string> Products { get; set; } 
        public bool IsCompleted { get; set; }
    }
}
