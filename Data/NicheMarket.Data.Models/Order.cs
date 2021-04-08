using AutoMapperConfiguration;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Data.Models
{
    public class Order: IMapTo<OrderViewModel>
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string RetailerId { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Column(TypeName = "decimal(28, 20)")]
        public decimal TotalPrice { get; set; }
        public List<OrderItem> Products { get; set; }
        public bool IsCompleted { get; set; }
    }
}
