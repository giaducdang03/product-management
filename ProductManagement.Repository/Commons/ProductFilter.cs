using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository.Commons
{
    public class ProductFilter
    {
        [FromQuery(Name = "search")]
        public string? Search { get; set; }

        [FromQuery(Name = "category")]
        public int? Category { get; set; }

        [FromQuery(Name = "sort-by")]
        public string? SortBy { get; set; }

        [FromQuery(Name = "dir")]
        public string? Dir {  get; set; }
    }
}
