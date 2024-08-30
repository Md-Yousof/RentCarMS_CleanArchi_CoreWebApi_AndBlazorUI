using Microsoft.EntityFrameworkCore;
using RentCarMS_CleanArchitecture.Application.Payments.PaymentServices;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Infrastructure.DATA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Infrastructure.Payments
{  
    public class PaymentRepository: IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public PaymentRepository(ApplicationDbContext context)
        {
             this._context = context;
            
        }
        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.payments
                .Include(p => p.RentCar)
                    .ThenInclude(rc => rc.Member)
                .Include(p => p.RentCar)
                    .ThenInclude(rc => rc.RentCarDetails)
                        .ThenInclude(rcd => rcd.Car) // Including Car navigation property
                .ToListAsync();
        }
        public async Task<Payment> CreateAsync(Payment payment)
        {
           _context.payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public Task<Payment> GetByIdAsyne(int a)
        {
            throw new NotImplementedException();
        }
    }
}
