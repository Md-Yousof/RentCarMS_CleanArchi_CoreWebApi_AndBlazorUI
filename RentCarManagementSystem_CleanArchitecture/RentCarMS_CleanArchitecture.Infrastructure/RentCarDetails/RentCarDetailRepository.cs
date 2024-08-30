using Microsoft.EntityFrameworkCore;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using RentCarMS_CleanArchitecture.Infrastructure.DATA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Infrastructure.RentCarDetails
{
    public class RentCarDetailRepository : IRentCarDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public RentCarDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RentCarDetail rentCarDetail)
        {
            await _context.rentCarDetails.AddAsync(rentCarDetail);
            //await _context.SaveChangesAsync();
        }

        public async Task<List<RentCarDetail>> GetAllIsReturnFalseAsync()
        {
           return await _context.rentCarDetails
                .Where(i=>i.IsReturn != true).ToListAsync();
        }

        public async Task<IEnumerable<RentCarDetail>> GetAllAsync()
        {
            return await _context.rentCarDetails
                 .Include(r => r.Car).
                 ToListAsync();
        }

        public async Task<RentCarDetail> GetByIdAsync(int id)
        {
            return await _context.rentCarDetails.FirstOrDefaultAsync(c => c.RentCarDetailID == id);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<RentCarDetail> UpdateAsync(RentCarDetail rentCarD)
        {
             _context.rentCarDetails.Update(rentCarD);
            await SaveChangesAsync();
            return rentCarD;
        }
        //public async Task UpdateAsync(RentCarDetail rentCarDetail)
        //{
        //    _context.Entry(rentCarDetail).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        public async Task<List<RentCarDetail>> GetRentCarDetailsByRentCarIdAsync(int rentCarId)
        {
            return await _context.rentCarDetails
                .Where(rcd => rcd.RentCarID == rentCarId)
                .ToListAsync();
        }
       
    }
}
