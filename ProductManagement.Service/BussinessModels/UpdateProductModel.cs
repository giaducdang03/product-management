using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.BussinessModels
{
    public class UpdateProductModel : CreateProductModel
    {
        [Required]
        public int Id { get; set; }
    }
}
