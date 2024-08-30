using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.Payments
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> CreateAsync(Payment payment);
        Task<Payment> GetByIdAsyne(int a);
       
        // Task SaveChangeAsync();

    }
}
