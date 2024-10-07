using ProductManagement.Repository.Interfaces;
using ProductManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Category> CategorysRepository { get; }
        IGenericRepository<Order> OrdersRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailsRepository { get; }
        IProductRepository ProductsRepository { get; }
        IMemberRepository MemberRepository { get; }
        int Save();
    }
}
