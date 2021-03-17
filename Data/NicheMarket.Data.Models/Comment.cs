using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Data.Models.Users
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ProductId { get; set; }

        [MaxLength(500)]
        public string Text { get; set; }
    }
}
