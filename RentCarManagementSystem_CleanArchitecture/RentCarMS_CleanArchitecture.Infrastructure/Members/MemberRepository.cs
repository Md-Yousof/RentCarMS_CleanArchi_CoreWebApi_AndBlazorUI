using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Infrastructure.DATA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RentCarMS_CleanArchitecture.Infrastructure.Members
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
           _context = context;
        }
        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.members.ToListAsync();
        }

        public async Task<Member> GetByIdAsync(int id)
        {
           return await _context.members.SingleOrDefaultAsync(m=>m.MemberID == id);
        }

        public async Task<Member> CreateAsync(Member member)
        {    
            _context.members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> UpdateAsync(Member member)
        {
            _context.members.Update(member);
            await _context.SaveChangesAsync();
            return member;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var deletemember = await GetByIdAsync(id);
            if (deletemember != null)
            {
                _context.members.Remove(deletemember);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
    }
}
