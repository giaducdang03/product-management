using AutoMapper;
using ProductManagement.Repository;
using ProductManagement.Repository.Commons;
using ProductManagement.Repository.Models;
using ProductManagement.Service.BussinessModels;
using ProductManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductModel> CreateProductAsync(CreateProductModel product)
        {
            var existCategory = await _unitOfWork.CategorysRepository.GetByIdAsync(product.CategoryId);
            if (existCategory == null) 
            {
                throw new Exception("Category does not exist.");
            }
            else
            {
                Product newProduct = _mapper.Map<Product>(product);
                await _unitOfWork.ProductsRepository.AddAsync(newProduct);
                _unitOfWork.Save();
                return _mapper.Map<ProductModel>(newProduct);
            }
        }

        public async Task<Pagination<ProductModel>> GetPagingProductsAsync(PaginationParameter paginationParameter)
        {
            var products = await _unitOfWork.ProductsRepository.ToPagination(paginationParameter);
            var productModels = _mapper.Map<List<ProductModel>>(products);
            return new Pagination<ProductModel>(productModels,
                products.TotalCount,
                products.CurrentPage,
                products.PageSize);
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            return product != null ? _mapper.Map<ProductModel>(product) : null;
        }
    }
}
