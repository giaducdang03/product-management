using ProductManagement.Repository.Interfaces;
using ProductManagement.Repository.Models;
using ProductManagement.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FstoreDbContext _context;
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<Member> _memberRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<OrderDetail> _orderDetailRepository;
        private IGenericRepository<Product> _productRepository;

        public UnitOfWork(FstoreDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Category> CategorysRepository
        {
            get
            {
                return _categoryRepository ??= new GenericRepository<Category>(_context);

            }
        }

        public IGenericRepository<Member> MembersRepository
        {
            get
            {
                return _memberRepository ??= new GenericRepository<Member>(_context);

            }
        }

        public IGenericRepository<Order> OrdersRepository
        {
            get
            {
                return _orderRepository ??= new GenericRepository<Order>(_context);

            }
        }

        public IGenericRepository<OrderDetail> OrderDetailsRepository
        {
            get
            {
                return _orderDetailRepository ??= new GenericRepository<OrderDetail>(_context);

            }
        }

        public IGenericRepository<Product> ProductsRepository
        {
            get
            {
                return _productRepository ??= new GenericRepository<Product>(_context);

            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
