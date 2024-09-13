using ProductManagement.Repository.Commons;
using ProductManagement.Repository.Models;
using ProductManagement.Service.BussinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryModel> GetCategoryByIdAsync(int id);

        Task<Pagination<CategoryModel>> GetPagingCategoriesAsync(PaginationParameter paginationParameter);

        Task<CategoryModel> CreateCategoryAsync(CreateCategoryModel categoryModel);

        Task<CategoryModel> UpdateCategoryAsync(CategoryModel categoryModel);

        Task<CategoryModel> DeleteCategoryAsync(int id);
    }
}
