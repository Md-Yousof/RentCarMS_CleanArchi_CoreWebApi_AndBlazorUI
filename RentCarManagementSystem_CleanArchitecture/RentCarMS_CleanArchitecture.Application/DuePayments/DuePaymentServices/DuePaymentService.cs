using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.DuePayments;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.DuePayments.DuePaymentServices
{
    public class DuePaymentService
    {
        private readonly IRentCarDetailRepository _DetailsRe;
        private readonly IPaymentRepository _paymentRe;

        public DuePaymentService(IRentCarDetailRepository DetailsRe, IPaymentRepository paymentRe)
        {
            _DetailsRe = DetailsRe;
            _paymentRe = paymentRe;
        }

        public async Task<IEnumerable<DuePaymentDto>> GetDuePayments()
        {
            var rentCarDetails = await _DetailsRe.GetAllAsync();
            var payments = await _paymentRe.GetAllAsync();

            var duePayments = new List<DuePaymentDto>();

            foreach (var rcd in rentCarDetails)
            {
                var daysPerInstallment = (rcd.EndDate - rcd.StartDate).TotalDays / rcd.NoOfInstallment.GetValueOrDefault(1);
                var totalInstallmentsPaid = payments
                    .Where(p => p.RentCarDetailID == rcd.RentCarDetailID)
                    .Sum(p => p.NofInstallMent.GetValueOrDefault(0) + p.AdvanceInstallMent.GetValueOrDefault(0));

                for (int installment = 1; installment <= rcd.NoOfInstallment; installment++)
                {
                    var dueDate = rcd.StartDate.AddDays(daysPerInstallment * (installment - 1));
                    var currentDateTime = DateTime.Now;

                    if (currentDateTime > dueDate && installment > totalInstallmentsPaid)
                    {
                        var dueAmount = rcd.MonthlyFeeInstallment.GetValueOrDefault(0);
                        duePayments.Add(new DuePaymentDto
                        {
                            RentCarID = rcd.RentCarID,
                            RentCarDetailID = rcd.RentCarDetailID,
                            DueDate = dueDate,
                            DueInstallment = installment,
                            DueAmount = dueAmount
                        });
                    }
                }
            }

            return duePayments;
        }



        //public async Task<IEnumerable<DuePaymentDto>> GetDuePayments()
        //{

        //    var rentCarDetails = await _DetailsRe.GetAllAsync();
        //    var payments = await _paymentRe.GetAllAsync();

        //    var duePayments = rentCarDetails.Select(rcd =>
        //    {
        //        var endDate = rcd.EndDate;
        //        var dateTime = DateTime.Now;

        //        if (dateTime <= endDate)
        //        {
        //            var days = (rcd.EndDate - rcd.StartDate).TotalDays / rcd.NoOfInstallment.GetValueOrDefault(1);
        //            var currentInstallment = (DateTime.Now - rcd.StartDate).TotalDays / days;

        //            var totalInstallmentsPaid = payments
        //                .Where(p => p.RentCarDetailID == rcd.RentCarDetailID)
        //                .Sum(p => p.NofInstallMent.GetValueOrDefault(0) + p.AdvanceInstallMent.GetValueOrDefault(0));

        //            var dueInstallment = currentInstallment - totalInstallmentsPaid;

        //            if (dueInstallment >= 1)
        //            {
        //                return new DuePaymentDto
        //                {
        //                    RentCarID = rcd.RentCarID,
        //                    RentCarDetailID = rcd.RentCarDetailID,
        //                    DueDate = rcd.EndDate.AddDays(-(rcd.NoOfInstallment.GetValueOrDefault(1) - (int)dueInstallment) * days),
        //                    DueInstallment = (int)dueInstallment,
        //                    DueAmount = (int)dueInstallment * rcd.MonthlyFeeInstallment.GetValueOrDefault(0)

        //                };
        //            }
        //        }

        //        if (dateTime > endDate)
        //        {
        //            var days = (rcd.EndDate - rcd.StartDate).TotalDays / rcd.NoOfInstallment.GetValueOrDefault(1);
        //            var currentInstallment = (rcd.EndDate - rcd.StartDate).TotalDays / days;

        //            var totalInstallmentsPaid = payments
        //                .Where(p => p.RentCarDetailID == rcd.RentCarDetailID)
        //                .Sum(p => p.NofInstallMent.GetValueOrDefault(0) + p.AdvanceInstallMent.GetValueOrDefault(0));

        //            var dueInstallment = currentInstallment - totalInstallmentsPaid;

        //            if (dueInstallment >= 1)
        //            {
        //                return new DuePaymentDto
        //                {
        //                    RentCarID = rcd.RentCarID,
        //                    RentCarDetailID = rcd.RentCarDetailID,
        //                    DueDate = rcd.EndDate.AddDays(-(rcd.NoOfInstallment.GetValueOrDefault(1) - (int)dueInstallment) * days),
        //                    DueInstallment = (int)dueInstallment,
        //                    DueAmount = (int)dueInstallment * rcd.MonthlyFeeInstallment.GetValueOrDefault(0)

        //                };
        //            }
        //        }
        //        return null; // Exclude items where dueInstallment <= 0
        //    })
        //    .Where(dp => dp != null)
        //    .ToList(); // Filter out null values

        //    return duePayments;
        //}

    }


}

    
