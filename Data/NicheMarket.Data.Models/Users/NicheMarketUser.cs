using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NicheMarket.Data.Models.Users
{
    // Add profile data for application users by adding properties to the NicheMarketUser class
    public class NicheMarketUser : IdentityUser<string>, User
    {
        public string Name { get ; set; }
        public string Password { get ; set ; }

        public string Address { get ; set ; }
        public string RoleName { get ; set ; }
    }
}
