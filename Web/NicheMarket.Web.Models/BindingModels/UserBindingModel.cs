using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Web.Models.BindingModels
{
    public class UserBindingModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; } 
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Name { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
