using Microsoft.AspNetCore.Http.HttpResults;
using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RentCarMS_CleanArchitecture.Application.RentCars.RentCarServices
{
    public class RentCarService
    {
        private readonly IRentCarRepository _rentCarrepository;
        private readonly IRentCarDetailRepository _rentCarDetailRepository;
        private readonly ICarRepository _carRepository;
        private readonly IPaymentRepository _paymentRepository;
        public RentCarService(IRentCarRepository rentCarrepository, IRentCarDetailRepository rdr, ICarRepository cr, IPaymentRepository paymentRepository)
        {
            _rentCarrepository = rentCarrepository;
            _rentCarDetailRepository = rdr;
            _carRepository = cr;
            _paymentRepository = paymentRepository;

        }

        public async Task<RentCarDTO> CreateRentCarAsync(RentCarDTO rentCarDto)
        {
            var rentCar = new RentCar
            {
                MemberID = rentCarDto.MemberID,
                RentCarDate = rentCarDto.RentCarDate,
                RentCarDetails = new List<RentCarDetail>()
            };

            foreach (var detailDto in rentCarDto.RentCarDetails)
            {
                var rentCarDetail = new RentCarDetail
                {
                    CarID = detailDto.CarID,
                    StartDate = detailDto.StartDate,
                    Duration = detailDto.Duration,
                    //Quantity = detailDto.Quantity,
                    Quantity = 1,
                    MonthlyFeeInstallment = detailDto.MonthlyFeeInstallment,
                    Status = "CarRentedNow",
                    IsReturn = false
                };
                rentCarDetail.SetEndDateAndInstallment();
               
                var car = await _carRepository.GetByIdAsync(rentCarDetail.CarID);

                var rentCarQuantityFalse = await _rentCarDetailRepository.GetAllAsync();
                var totalRentedQuantity = rentCarQuantityFalse
                    .Where(d => d.CarID == car.CarID && d.IsReturn != true)
                    .Sum(d => d.Quantity.GetValueOrDefault(0));

                if (rentCarDetail.Quantity > (car.Quentity - totalRentedQuantity))
                {
                    throw new ArgumentException("Pleace decrease Car Quantity to balance");
                }
              
                rentCar.RentCarDetails.Add(rentCarDetail);
            }

            await _rentCarrepository.CreateAsync(rentCar);
            await _rentCarrepository.SaveChangesAsync();

            foreach (var det in rentCar.RentCarDetails)
            {
                var car = await _carRepository.GetByIdAsync(det.CarID);
                if (car != null)
                {
                    var rentCarQuantityFalse = await _rentCarDetailRepository.GetAllAsync();
                    var totalRentedQuantity = rentCarQuantityFalse
                        .Where(d => d.CarID == car.CarID && d.IsReturn != true)
                        .Sum(d => d.Quantity.GetValueOrDefault(0));
                    //car.Status = await UpdateCarStatusAsync(car, det);

                    if (car.Quentity == totalRentedQuantity)
                    {
                        car.Status = false;
                                            
                    }
                    else
                    {
                        car.Status = true;
                       
                    }
                    await _carRepository.UpdateAsync(car);
                }
               
            }
          
            return new RentCarDTO
            {
                RentCarID = rentCar.RentCarID,
                MemberID = rentCar.MemberID,
                RentCarDate = rentCar.RentCarDate,
                RentCarDetails = rentCar.RentCarDetails.Select(d => new RentCarDetailDTO
                {
                    RentCarDetailID = d.RentCarDetailID,
                    RentCarID = d.RentCarID,
                    CarID = d.CarID,
                    Duration = d.Duration,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Quantity = d.Quantity,
                    MonthlyFeeInstallment = d.MonthlyFeeInstallment,
                    TotalFee = d.TotalFee,
                    NoOfInstallment = d.NoOfInstallment,
                    IsReturn = d.IsReturn
                }).ToList()
            };
        }



        private async Task<bool> CarQuantityAvailable(Car car, RentCarDetail rentCarDetail)
        {
            var rentCarDetails = await _rentCarDetailRepository.GetAllAsync();
            var totalRentedQuantity = rentCarDetails
                .Where(d => d.CarID == car.CarID && d.IsReturn !=true)
                .Sum(d => d.Quantity.GetValueOrDefault(0));


            if (car.Quentity <= totalRentedQuantity)
            {
                car.Status = false;
            }
            else
            {
                car.Status = true;
            }
            return car.Status ?? true;
        }


        private async Task<bool> UpdateCarStatusAsync(Car car, RentCarDetail rentCarDetail)
        {
            var rentCarDetails = await _rentCarDetailRepository.GetAllAsync();
            var totalRentedQuantity = rentCarDetails
                .Where(d => d.CarID == car.CarID && d.IsReturn !=true)
                .Sum(d => d.Quantity.GetValueOrDefault(0));
            if (car.Quentity <= totalRentedQuantity)
            {
                car.Status = false;
            }
            else 
            {
                car.Status = true;
            }
            return car.Status ?? false;
        }

        public async Task<RentCarDTO> UpdateRentCarAsync(RentCarDTO rentCarDto)
        {
            var rentCar = await _rentCarrepository.GetByIdAsync(rentCarDto.RentCarID);

            if (rentCar == null)
            {
                return null;
            }

            rentCar.MemberID = rentCarDto.MemberID;
            rentCar.RentCarDate = rentCarDto.RentCarDate;
            rentCar.RentCarDetails = rentCarDto.RentCarDetails?.Select(detailDto =>
            {
                var detail = new RentCarDetail
                {
                    RentCarDetailID = detailDto.RentCarDetailID,
                    CarID = detailDto.CarID,
                    StartDate = detailDto.StartDate,
                    Duration = detailDto.Duration,
                    Quantity = detailDto.Quantity,
                    MonthlyFeeInstallment = detailDto.MonthlyFeeInstallment,
                    IsReturn = detailDto.IsReturn
                };

                detail.SetEndDateAndInstallment();
                return detail;
            }).ToList();

            await _rentCarrepository.UpdateAsync(rentCar);
            await _rentCarrepository.SaveChangesAsync();

            foreach (var detail in rentCar.RentCarDetails)
            {
                var car = await _carRepository.GetByIdAsync(detail.CarID);
                if (car != null)
                {
                    car.Status = await UpdateCarStatusAsync(car, detail);
                    await _carRepository.UpdateAsync(car);
                }               
            }

            return new RentCarDTO
            {
                RentCarID = rentCar.RentCarID,
                MemberID = rentCar.MemberID,
                RentCarDate = rentCar.RentCarDate,
                RentCarDetails = rentCar.RentCarDetails.Select(d => new RentCarDetailDTO
                {
                    RentCarDetailID = d.RentCarDetailID,
                    RentCarID = d.RentCarID,
                    CarID = d.CarID,
                    Duration = d.Duration,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Quantity = d.Quantity,
                    MonthlyFeeInstallment = d.MonthlyFeeInstallment,
                    TotalFee = d.TotalFee,
                    NoOfInstallment = d.NoOfInstallment,
                    IsReturn = d.IsReturn
                }).ToList()
            };
        }

        public async Task<IEnumerable<RentCarDTO>> GetAllRentCarsAsync()
        {
            var rentCars = await _rentCarrepository.GetAllAsync();         
            return rentCars.Select(r => new RentCarDTO
            {
                RentCarID = r.RentCarID,
                MemberID = r.MemberID,
                RentCarDate = r.RentCarDate,
                RentCarDetails = r.RentCarDetails?.Select(d => new RentCarDetailDTO
                {
                    RentCarDetailID = d.RentCarDetailID,
                    RentCarID = d.RentCarID,
                    CarID = d.CarID,
                    Duration = d.Duration,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Quantity = d.Quantity,
                    MonthlyFeeInstallment = d.MonthlyFeeInstallment,
                    TotalFee = d.TotalFee,
                    NoOfInstallment = d.NoOfInstallment,
                    IsReturn = d.IsReturn
                }).ToList()

            }).ToList();
        }


        public async Task<RentCarDTO> GetRentCarById(int id)
        {
            var r = await _rentCarrepository.GetByIdAsync(id);
            if (r == null)
            {
                return null;
            }

            return new RentCarDTO
            {
                RentCarID = r.RentCarID,
                MemberID = r.MemberID,
                RentCarDate = r.RentCarDate,
                RentCarDetails = r.RentCarDetails?.Select(d => new RentCarDetailDTO
                {
                    RentCarDetailID = d.RentCarDetailID,
                    RentCarID = d.RentCarID,
                    CarID = d.CarID,
                    Duration = d.Duration,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    Quantity = d.Quantity,
                    MonthlyFeeInstallment = d.MonthlyFeeInstallment,
                    TotalFee = d.TotalFee,
                    NoOfInstallment = d.NoOfInstallment,
                    IsReturn = d.IsReturn
                }).ToList(),
            };

        }

        public async Task<bool> DeleteRentCar(int id)
        {
            var rentCar = await _rentCarrepository.GetByIdAsync(id);
            var rentcarDetail = rentCar.RentCarDetails;

            var result = await _rentCarrepository.DeleteAsync(id);      
            if (!result)
            {
                return false;
            }

            // Update car status
            foreach (var detail in rentcarDetail)
            {
                var car = await _carRepository.GetByIdAsync(detail.CarID);
                if (car != null)
                {
                    car.Status = await UpdateCarStatusAsync(car, detail);
                    await _carRepository.UpdateAsync(car);
                }
            }

            return true;
        }


        public async Task<IEnumerable<RentCarDTO>> GetAllRentCarsWithUpdatedInstallmentsAsync()
        {
        
            var rentCars = await _rentCarrepository.GetAllAsync();

            var payments = await _paymentRepository.GetAllAsync();

            // Create a list of RentCarDTO with updated NoOfInstallment
            var rentCarDTOs = rentCars.Select(r => new RentCarDTO
            {
                RentCarID = r.RentCarID,
                MemberID = r.MemberID,
                RentCarDate = r.RentCarDate,
                RentCarDetails = r.RentCarDetails?.Select(d =>
                {
                  
                    var totalInstallmentsPaid = payments
                        .Where(p => p.RentCarDetailID == d.RentCarDetailID)
                          .Sum(p => p.NofInstallMent.GetValueOrDefault(0) + p.AdvanceInstallMent.GetValueOrDefault(0));

                    var daysDifference = (d.EndDate - DateTime.Today).TotalDays;


                    return new RentCarDetailDTO
                    {
                        RentCarDetailID = d.RentCarDetailID,
                        RentCarID = d.RentCarID,
                        CarID = d.CarID,
                        Duration = d.Duration,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        Quantity = d.Quantity,
                        MonthlyFeeInstallment = d.MonthlyFeeInstallment,
                        TotalFee = d.TotalFee,
                        NoOfInstallment = d.NoOfInstallment.HasValue
                            ? d.NoOfInstallment.Value - totalInstallmentsPaid
                            : (int?)null,
                        Status = daysDifference <= 0 ? "EndDate Over" : "Not EndDate Over",
                        IsReturn = d.IsReturn
                    };
                }).ToList()
            }).ToList();

            return rentCarDTOs;
        }

        public async Task<List<RentCarDTO>> GetRentCarsWithMemberInfo()
        {
            var rentCars = await _rentCarrepository.GetAllAsync();

            return rentCars.Select(rc => new RentCarDTO
            {
                RentCarID = rc.RentCarID,
                MemberInfo = $"{rc.Member?.FastName} {rc.Member?.LastName} :RI-{rc.RentCarID}"
            }).ToList();
        }

    }

}




