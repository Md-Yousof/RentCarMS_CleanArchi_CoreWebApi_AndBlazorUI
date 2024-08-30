using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.Members
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllAsynce();
        Task<Member> GetByIdAsynce(int id);
        Task CreateAsynce(Member content);

        Task UpdateAsync(Member memberDto);
        Task DeleteAsynce(int id);
    }
}
