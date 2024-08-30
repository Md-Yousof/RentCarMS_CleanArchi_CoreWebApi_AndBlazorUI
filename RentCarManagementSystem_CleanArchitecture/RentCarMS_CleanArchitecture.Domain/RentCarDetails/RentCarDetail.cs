using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.RentCarDetails
{
    public class RentCarDetail
    {
        [Key]
        public int RentCarDetailID { get; set; }
        public int RentCarID { get; set; }
        public RentCar? RentCar { get; set; }
        public int CarID { get; set; }
        public Car? Car { get; set; }
        public int? Duration { get; set; } = 1;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int? Quantity { get; set; } = 1;
        public decimal? MonthlyFeeInstallment { get; set; }
        public decimal? TotalFee { get; private set; }
        public int? NoOfInstallment { get; private set; }
        public string? Status { get; set; }
        public bool? IsReturn { get; set; }

        //[JsonIgnore]
        //public Payment? Payment { get; set; }
        public void SetEndDateAndInstallment()
        {
            if (Duration.HasValue)
            {
                EndDate = StartDate.AddYears(Duration.Value);
            }
            CalculateNoOfInstallments();
            UpdateTotalFee();
        }

        private void CalculateNoOfInstallments()
        {
            NoOfInstallment = ((EndDate.Year - StartDate.Year) * 12) + EndDate.Month - StartDate.Month;
        }

        public void UpdateTotalFee()
        {
            if (NoOfInstallment.HasValue && MonthlyFeeInstallment.HasValue && Quantity.HasValue)
            {
                TotalFee = MonthlyFeeInstallment.Value * NoOfInstallment.Value * Quantity.Value;
            }
            else
            {
                TotalFee = null;
            }
        }

        //public void SetQuantityAndDuration(int? quantity, int? duration)
        //{
        //    Quantity = quantity ?? 1;
        //    Duration = duration ?? 1;
        //}

       
    }
}
