using AutoMapperConfiguration;
using NicheMarket.Data.Models;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Services.Models
{
    public class OrderServiceModel : IMapTo<Order>,  IMapFrom<CreateOrderBindingModel>
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string RetailerId { get; set; }
        public string Adress { get; set; }
        public string ClientName { get; set; }
        public double TotalPrice { get; set; }
        public List<string> Products { get; set; }
        public bool IsCompleted { get; set; }
    }
}
