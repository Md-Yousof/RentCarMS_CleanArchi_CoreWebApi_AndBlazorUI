using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Infrastructure.DATA.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Infrastructure.Cars
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Car> CreateAsync(Car car)
        {
                  _context.cars.Add(car);
            await _context.SaveChangesAsync();
            return car;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var car = await GetByIdAsync(id);
            if(car != null)
            {
                _context.cars.Remove(car);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
            
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.cars.ToListAsync();

        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.cars.FirstOrDefaultAsync(c => c.CarID == id);
        }

        public async Task<Car> UpdateAsync(Car car)
        {
          
                _context.cars.Update(car); 
                await _context.SaveChangesAsync();
                return car;
          
        }

        public async Task<List<Car>> GetByStatusTrue()
        {
            return await _context.cars
                 .Where(c => c.Status == true)
                 .ToListAsync();
        }
    }
}
