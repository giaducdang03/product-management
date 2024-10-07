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

        public async Task<ProductModel> DeleteProductAsync(int productId)
        {
            var deleteProduct = await _unitOfWork.ProductsRepository.GetByIdAsync(productId);
            if (deleteProduct == null)
            {
                throw new Exception("Not found product. Can not delete.");
            }

            _unitOfWork.ProductsRepository.Remove(deleteProduct);
            _unitOfWork.Save();
            return _mapper.Map<ProductModel>(deleteProduct);
        }

        public async Task<Pagination<ProductModel>> GetPagingProductsAsync(PaginationParameter paginationParameter, ProductFilter productFilter)
        {
            var products = await _unitOfWork.ProductsRepository.GetProductPaging(paginationParameter, productFilter);
            var productModels = _mapper.Map<List<ProductModel>>(products);
            return new Pagination<ProductModel>(productModels,
                products.TotalCount,
                products.CurrentPage,
                products.PageSize);
        }

        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetProductById(id);
            return product != null ? _mapper.Map<ProductModel>(product) : null;
        }

        public async Task<ProductModel> UpdateProductAsync(UpdateProductModel productModel)
        {
            var updateProduct = await _unitOfWork.ProductsRepository.GetByIdAsync(productModel.Id);
            if (updateProduct == null)
            {
                throw new Exception("Product does not exist.");
            }

            var existProducts = await _unitOfWork.ProductsRepository.GetAllAsync();
            if (existProducts.Any())
            {
                if (existProducts.Where(x => x.ProductName == productModel.ProductName).Any())
                {
                    throw new Exception("Product is already exist.");
                }
            }

            var category = await _unitOfWork.CategorysRepository.GetByIdAsync(productModel.CategoryId);
            if (category == null)
            {
                throw new Exception("Category does not exist.");
            }

            updateProduct.ProductName = productModel.ProductName;
            updateProduct.CategoryId = productModel.CategoryId;
            updateProduct.Weight = productModel.Weight;
            updateProduct.UnitPrice = productModel.UnitPrice;
            updateProduct.UnitsInStock = productModel.UnitsInStock;
            
            _unitOfWork.ProductsRepository.UpdateAsync(updateProduct);
            _unitOfWork.Save();
            return _mapper.Map<ProductModel>(updateProduct);
        }
    }
}
