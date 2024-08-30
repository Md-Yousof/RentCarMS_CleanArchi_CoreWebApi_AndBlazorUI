using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.RentCarDetails.RentCarDetailServices
{
    public class RentCarDetailService
    {
        public readonly IRentCarDetailRepository _repository;

        public RentCarDetailService(IRentCarDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RentCarDetailDTO>> GetAllIsReturnFalse()
        {
            var isReturn = await _repository.GetAllIsReturnFalseAsync();

            return isReturn.Select(x => new RentCarDetailDTO 
            {
                 
                RentCarDetailID = x.RentCarDetailID,
                RentCarID = x.RentCarID,
                CarID = x.CarID,
                Duration = x.Duration,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Quantity = x.Quantity,
                MonthlyFeeInstallment = x.MonthlyFeeInstallment,
                TotalFee = x.TotalFee,
                NoOfInstallment = x.NoOfInstallment,
                Status = x.Status,
                IsReturn = x.IsReturn
            }).ToList();
        }

        public async Task<List<RentCarDetailDTO>> GetAllDetailsAsynce()
        {
            var isReturn = await _repository.GetAllAsync();

            return isReturn.Select(x => new RentCarDetailDTO
            {

                RentCarDetailID = x.RentCarDetailID,
                RentCarID = x.RentCarID,
                CarID = x.CarID,
                Duration = x.Duration,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Quantity = x.Quantity,
                MonthlyFeeInstallment = x.MonthlyFeeInstallment,
                TotalFee = x.TotalFee,
                NoOfInstallment = x.NoOfInstallment,
                Status = x.Status,
                IsReturn = x.IsReturn
            }).ToList();
        }


        public async Task<List<RentCarDetailDTO>> GetRentCarDetailsByRentCarIdAsync(int rentCarId)
        {
            var rentCarDetails = await _repository.GetRentCarDetailsByRentCarIdAsync(rentCarId);

            return rentCarDetails.Select(x => new RentCarDetailDTO
            {
                RentCarDetailID = x.RentCarDetailID,
                RentCarID = x.RentCarID,
                CarID = x.CarID,
                Duration = x.Duration,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Quantity = x.Quantity,
                MonthlyFeeInstallment = x.MonthlyFeeInstallment,
                TotalFee = x.TotalFee,
                NoOfInstallment = x.NoOfInstallment,
                Status = x.Status,
                IsReturn = x.IsReturn
            }).ToList();
        }


        public async Task<List<RentCarDetailDTO>> GetCarInfoListForCreatePayment()
        {
            var rentCars = await _repository.GetAllAsync();

            return rentCars.Select(rc => new RentCarDetailDTO
            {
                RentCarDetailID = rc.RentCarDetailID,
                RentCarID = rc.RentCarID,
                //CarID = rc.CarID,
                MonthlyFeeInstallment = rc.MonthlyFeeInstallment,
                CarInfo = $"{rc.Car?.LicensePlaete}-{rc.Car?.Model}"
            }).ToList();
        }
    }

}

