using Microsoft.EntityFrameworkCore;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using RentCarMS_CleanArchitecture.Infrastructure.DATA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Infrastructure.RentCars
{
    public class RentCarRepository : IRentCarRepository
    {
        private readonly ApplicationDbContext _context;
        public RentCarRepository(ApplicationDbContext context) 
        {
            _context = context;
        } 
        public async Task CreateAsync(RentCar rentCar)
        {
             await _context.rentCars.AddAsync(rentCar);
                     
        }


        //public async Task<IEnumerable<RentCar>> GetAllAsync()
        //{
        //    return await _context.rentCars.Include(rcd=>rcd.RentCarDetails).ToListAsync();

        //}
        public async Task<IEnumerable<RentCar>> GetAllAsync()
        {
            return await _context.rentCars
                .Include(m => m.Member)
                .Include(rcd => rcd.RentCarDetails)
                .ToListAsync();

        }

        public async Task<RentCar> GetByIdAsync(int id)
        {
            return await _context.rentCars
                .Include(rcd => rcd.RentCarDetails)
                .FirstOrDefaultAsync(r => r.RentCarID == id);
              
        }
        //public async Task<RentCar> GetByIdAsync(int id)
        //{
        //    return await _context.rentCars
        //        .Include(rcd => rcd.RentCarDetails)
        //        .Include(rcd => rcd.RentCarDetails).FirstOrDefaultAsync(r => r.RentCarID == id);

        //}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<RentCar> UpdateAsync(RentCar rentCar)
        {
              _context.rentCars.Update(rentCar);
              await SaveChangesAsync();
             return rentCar;           
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var rc = await GetByIdAsync(id);
            if (rc != null)
            {
                _context.Remove(rc);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //public async Task<List<RentCar>> GetAllDetailIdBySameRentCarIdAsync()
        //{
        //    return await _context.rentCars.Include(d => d.RentCarDetails)
        //        .Where(r => r.RentCarDetails.Any(i => i.IsReturn == false))
        //        .ToListAsync();

        //}


    }
}
