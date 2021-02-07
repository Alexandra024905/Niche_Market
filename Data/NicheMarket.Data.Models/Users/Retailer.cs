using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Data.Models.Users
{
    public class Retailer: NicheMarketUser, User
    {
        public  List<Product> Products { get; set; }
 
    }
}
