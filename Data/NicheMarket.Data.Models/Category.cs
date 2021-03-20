using AutoMapperConfiguration;
using NicheMarket.Web.Models.BindingModels;
using NicheMarket.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicheMarket.Data.Models
{
    public class Category: IMapTo<CategoryViewModel>, IMapFrom<CreateCategoryModel>
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
