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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
