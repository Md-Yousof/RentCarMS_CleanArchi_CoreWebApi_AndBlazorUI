using RentCarMS_CleanArchitecture.Domain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.Cars
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> GetByIdAsync(int id);
        Task<Car> CreateAsync(Car member);
        Task<Car> UpdateAsync(Car member);
        Task<bool> DeleteAsync(int id);

        Task<List<Car>> GetByStatusTrue();
    }
}
