using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.Payments
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task CreatePayment(Payment payment);

        Task<List<Payment>> GetMemberInfoInListPaymentByRentCarId();
    }
}
