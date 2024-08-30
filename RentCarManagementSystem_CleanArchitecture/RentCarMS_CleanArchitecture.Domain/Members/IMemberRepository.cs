using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.Members
{
    public interface IMemberRepository
    {
       Task<IEnumerable<Member>> GetAllAsync();
       Task<Member> GetByIdAsync(int id);
       Task<Member> CreateAsync (Member member);
       Task<Member> UpdateAsync (Member member);
       Task<bool> DeleteAsync (int id);
    }
}
