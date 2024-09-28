using ProductManagement.Repository.Commons;
using ProductManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Pagination<Product>> GetProductPaging(PaginationParameter paginationParameter, ProductFilter productFilter);

        Task<Product> GetProductById(int id);
    }
}
