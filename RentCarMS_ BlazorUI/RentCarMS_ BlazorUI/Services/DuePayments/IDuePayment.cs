using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.DuePayments
{
    public interface IDuePayment
    {
        Task<IEnumerable<DuePayment>> GetAllAsynce();
    }
}
