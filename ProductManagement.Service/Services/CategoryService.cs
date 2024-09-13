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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryModel> CreateCategoryAsync(CreateCategoryModel categoryModel)
        {
            var existCategories = await _unitOfWork.CategorysRepository.GetAllAsync();
            if (existCategories.Any()) 
            {
                if (existCategories.Where(x => x.CategoryName == categoryModel.Categoryname).Any()) 
                {
                    throw new Exception("Category is already exist.");
                }
            }
            Category newCategory = new Category
            {
                CategoryName = categoryModel.Categoryname,
            };

            await _unitOfWork.CategorysRepository.AddAsync(newCategory);
            _unitOfWork.Save();
            return _mapper.Map<CategoryModel>(newCategory);
        }

        public async Task<CategoryModel> DeleteCategoryAsync(int categoryId)
        {
            var deleteCategory = await _unitOfWork.CategorysRepository.GetByIdAsync(categoryId);
            if (deleteCategory == null)
            {
                throw new Exception("Not found category. Can not delete.");
            }

            var listProduct = await _unitOfWork.ProductsRepository.GetAllAsync();
            if (listProduct.Any()) 
            { 
                if (listProduct.FirstOrDefault(x => x.CategoryId == categoryId) != null)
                {
                    throw new Exception("There is a product in this category. Can not delete.");
                }
            }

            _unitOfWork.CategorysRepository.Remove(deleteCategory);
            _unitOfWork.Save();
            return _mapper.Map<CategoryModel>(deleteCategory);
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategorysRepository.GetByIdAsync(id);
            return category != null ? _mapper.Map<CategoryModel>(category) : null;
        }

        public async Task<Pagination<CategoryModel>> GetPagingCategoriesAsync(PaginationParameter paginationParameter)
        {
            var categories = await _unitOfWork.CategorysRepository.ToPagination(paginationParameter);
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);
            return new Pagination<CategoryModel>(categoryModels,
                categories.TotalCount,
                categories.CurrentPage,
                categories.PageSize);
        }

        public async Task<CategoryModel> UpdateCategoryAsync(CategoryModel categoryModel)
        {
            var updateCategory = await _unitOfWork.CategorysRepository.GetByIdAsync(categoryModel.CategoryId);
            if (updateCategory == null)
            {
                throw new Exception("Category does not exist.");
            }

            var existCategories = await _unitOfWork.CategorysRepository.GetAllAsync();
            if (existCategories.Any())
            {
                if (existCategories.Where(x => x.CategoryName == categoryModel.CategoryName).Any())
                {
                    throw new Exception("Category is already exist.");
                }
            }

            updateCategory.CategoryName = categoryModel.CategoryName;
            _unitOfWork.CategorysRepository.UpdateAsync(updateCategory);
            _unitOfWork.Save();
            return _mapper.Map<CategoryModel>(updateCategory);
        }
    }
}
