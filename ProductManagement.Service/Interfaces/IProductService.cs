using ProductManagement.Repository.Commons;
using ProductManagement.Service.BussinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductModel> GetProductByIdAsync(int id);

        Task<Pagination<ProductModel>> GetPagingProductsAsync(PaginationParameter paginationParameter);

        Task<ProductModel> CreateProductAsync(CreateProductModel product);
    }
}
