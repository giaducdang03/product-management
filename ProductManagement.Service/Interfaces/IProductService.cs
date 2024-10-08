﻿using ProductManagement.Repository.Commons;
using ProductManagement.Repository.Models;
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

        Task<Pagination<ProductModel>> GetPagingProductsAsync(PaginationParameter paginationParameter, ProductFilter productFilter);

        Task<ProductModel> CreateProductAsync(CreateProductModel product);

        Task<ProductModel> UpdateProductAsync(UpdateProductModel productModel);

        Task<ProductModel> DeleteProductAsync(int productId);
    }
}
