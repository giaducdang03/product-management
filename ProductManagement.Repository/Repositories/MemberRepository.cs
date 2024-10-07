using Microsoft.EntityFrameworkCore;
using ProductManagement.Repository.Interfaces;
using ProductManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Repository.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        private readonly FstoreDbContext _context;

        public MemberRepository(FstoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Member> GetMemberByEmail(string email)
        {
            return await _context.Members.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
