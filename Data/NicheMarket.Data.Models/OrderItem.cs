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
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }
    }
}
