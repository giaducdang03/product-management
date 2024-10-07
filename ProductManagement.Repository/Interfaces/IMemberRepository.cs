using ProductManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member> GetMemberByEmail(string email);
    }
}
