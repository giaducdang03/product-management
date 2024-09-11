using AutoMapper;
using ProductManagement.Repository;
using ProductManagement.Repository.Commons;
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
