using Microsoft.AspNetCore.Http.HttpResults;
using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.Payments.PaymentServices
{
    public class PaymentService
    {
        private readonly IRentCarDetailRepository _rentCarDetailRepository;
        private readonly IPaymentRepository _repository;
        public PaymentService(IPaymentRepository repository, IRentCarDetailRepository rentCarDetailRepository)
        {
            _repository = repository;
            _rentCarDetailRepository = rentCarDetailRepository;
            
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPayment()
        {
            var payments = await _repository.GetAllAsync();
            return payments.Select(p => new PaymentDto
            {
                PaymentID = p.PaymentID,
                RentCarID = p.RentCarID,
                RentCarDetailID = p.RentCarDetailID,
                PaymentDate = p.PaymentDate,
                NofInstallMent = p.NofInstallMent,
                PaidAmmount = p.PaidAmmount,
                AdvanceInstallMent = p.AdvanceInstallMent,

            }).ToList();
        }

        public async Task<List<PaymentDto>> GetMemberInfoInListPaymentByRentCarId()
        {
            var paymentAll = await _repository.GetAllAsync();

            return paymentAll.Select(rc => new PaymentDto
            {
                PaymentID = rc.PaymentID,
                RentCarID = rc.RentCarID,
                RentCarDetailID = rc.RentCarDetailID,
                PaymentDate = rc.PaymentDate,
                NofInstallMent = rc.NofInstallMent,
                AdvanceInstallMent = rc.AdvanceInstallMent,
                PaidAmmount = rc.PaidAmmount,
                MemberInfo = $"{rc.RentCar?.Member?.FastName} {rc.RentCar?.Member?.LastName} :RI-{rc.RentCarID}",

                CarInfo = rc.RentCar?.RentCarDetails?
               .FirstOrDefault(r => r.RentCarDetailID == rc.RentCarDetailID) != null
               ? $"{rc.RentCar?.RentCarDetails.FirstOrDefault(r => r.RentCarDetailID == rc.RentCarDetailID)?.Car?.LicensePlaete}-" +
                 $"{rc.RentCar?.RentCarDetails.FirstOrDefault(r => r.RentCarDetailID == rc.RentCarDetailID)?.Car?.Model}, " //+
                // $"{rc.RentCar?.RentCarDetails.FirstOrDefault(r => r.RentCarDetailID == rc.RentCarDetailID)?.Car?.Color}"
               : rc.RentCarDetailID.ToString()


            }).ToList();
        }






        public async Task<PaymentDto> CreatePayment(PaymentDto paymentDto)
        {
            var rentCarDetail = await _rentCarDetailRepository.GetByIdAsync(paymentDto.RentCarDetailID);
            if (rentCarDetail == null)
            {
                throw new Exception("RentCarDetail not found");
            }

            var paidAmount = rentCarDetail.MonthlyFeeInstallment.GetValueOrDefault(0) *
                             (paymentDto.NofInstallMent.GetValueOrDefault(0) + paymentDto.AdvanceInstallMent.GetValueOrDefault(0));

            var payment = new Payment
            {

                RentCarID = paymentDto.RentCarID,
                RentCarDetailID = paymentDto.RentCarDetailID,
                PaymentDate = paymentDto.PaymentDate,
                NofInstallMent = paymentDto.NofInstallMent,
                PaidAmmount = paidAmount,
                AdvanceInstallMent = paymentDto.AdvanceInstallMent
            };

            var payid = await _repository.CreateAsync(payment);
            paymentDto.PaymentID = payid.PaymentID;
            paymentDto.PaidAmmount = paidAmount;
            return paymentDto;
        }
    }

    

}
