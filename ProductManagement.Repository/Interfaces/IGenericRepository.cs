using Microsoft.EntityFrameworkCore.Storage;
using ProductManagement.Repository.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        void UpdateAsync(T entity);
        void Remove(T entity);
        Task AddRangeAsync(List<T> entities);
        void RemoveRange(List<T> entities);
        Task<Pagination<T>> ToPagination(PaginationParameter paginationParameter);

    }
}
