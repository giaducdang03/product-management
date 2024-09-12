using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.BussinessModels
{
    public class CreateProductModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d{1,5}(g|kg|mg|ml|l)$", ErrorMessage = "Weight must be in the format like '300g', where the number can have up to 5 digits.")]
        public string Weight { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice must be greater than 0.")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "UnitsInStock minimum is 0.")]
        public int UnitsInStock { get; set; }
    }
}
