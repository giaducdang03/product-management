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

        public async Task<Pagination<Product>> GetProductPaging(PaginationParameter paginationParameter, ProductFilter productFilter)
        {
            var query = _context.Products.Include(x => x.Category).AsQueryable();

            // apply filter
            query = ApplyFiltering(query, productFilter);

            var itemCount = await query.CountAsync();
            var items = await query.Skip((paginationParameter.PageIndex - 1) * paginationParameter.PageSize)
                                    .Take(paginationParameter.PageSize)
                                    .AsNoTracking()
                                    .ToListAsync();
            var result = new Pagination<Product>(items, itemCount, paginationParameter.PageIndex, paginationParameter.PageSize);

            return result;
        }

        public IQueryable<Product> ApplyFiltering(IQueryable<Product> query, ProductFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(s => s.ProductName.Contains(filter.Search));
            }

            if (filter.Category != null)
            {
                query = query.Where(s => s.CategoryId == filter.Category);
            }

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "name":
                        query = filter.Dir?.ToLower() == "desc" ? query.OrderByDescending(s => s.ProductName) : query.OrderBy(s => s.ProductName);
                        break;
                    case "price":
                        query = filter.Dir?.ToLower() == "desc" ? query.OrderByDescending(s => s.UnitPrice) : query.OrderBy(s => s.UnitPrice);
                        break;
                    default:
                        query = query.OrderBy(s => s.ProductId); // Default sort by Id
                        break;
                }
            }

            return query;
        }
    }
}
