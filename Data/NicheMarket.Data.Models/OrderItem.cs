using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Data.Models
{
    public class OrderItem
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string OrderId { get; set; }
    }
}
