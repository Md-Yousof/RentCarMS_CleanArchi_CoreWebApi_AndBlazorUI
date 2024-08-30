using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.RentCarDetails
{
    public interface IRentCarDetailRepository
    {

        Task CreateAsync(RentCarDetail rentCarDetail);
        Task<RentCarDetail> UpdateAsync(RentCarDetail rentCar);
        Task<IEnumerable<RentCarDetail>> GetAllAsync();
        Task SaveChangesAsync();

        Task<RentCarDetail> GetByIdAsync(int id);
        Task<List<RentCarDetail>> GetAllIsReturnFalseAsync();

        Task<List<RentCarDetail>> GetRentCarDetailsByRentCarIdAsync(int rentCarId);


    }
}
