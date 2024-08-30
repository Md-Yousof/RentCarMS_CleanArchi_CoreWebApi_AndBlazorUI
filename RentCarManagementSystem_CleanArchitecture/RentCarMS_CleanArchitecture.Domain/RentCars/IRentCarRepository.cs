using RentCarMS_CleanArchitecture.Domain.Payments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.RentCars
{
    public interface IRentCarRepository
    {
        Task CreateAsync(RentCar rentCar);
        Task<IEnumerable<RentCar>> GetAllAsync();
        Task<RentCar> GetByIdAsync(int id);
        Task<RentCar> UpdateAsync(RentCar rentCar);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();

        //Task<List<RentCar>> GetAllDueNofInstallment();
        //Task<List<RentCar>> GetAllDetailIdBySameRentCarIdAsync();

    }
}
