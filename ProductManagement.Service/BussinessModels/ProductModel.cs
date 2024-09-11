using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.BussinessModels
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public string? CategoryName { get; set; }

        public string ProductName { get; set; } = null!;

        public string Weight { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }
    }
}
