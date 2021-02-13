using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Web.Models.BindingModels
{
    public class ShoppingCartItem
    {
        public ProductViewModel Product { get; set; }

        public int Quantity { get; set; }

    }
}
