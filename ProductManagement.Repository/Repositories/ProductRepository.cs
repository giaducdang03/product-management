using Microsoft.EntityFrameworkCore;
using ProductManagement.Repository.Commons;
using ProductManagement.Repository.Interfaces;
using ProductManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly FstoreDbContext _context;

        public ProductRepository(FstoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<Pagination<Product>> GetProductPaging(PaginationParameter paginationParameter)
        {
            var itemCount = await _context.Products.CountAsync();
            var items = await _context.Products.Skip((paginationParameter.PageIndex - 1) * paginationParameter.PageSize)
                                    .Take(paginationParameter.PageSize)
                                    .AsNoTracking()
                                    .ToListAsync();
            var result = new Pagination<Product>(items, itemCount, paginationParameter.PageIndex, paginationParameter.PageSize);

            return result;
        }
    }
}
