using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.DTO_s
{
    public class RentCarDetailDTO
    {
        public int RentCarDetailID { get; set; }
        public int RentCarID { get; set; }       
        public int CarID { get; set; }    
        public int? Duration { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalFee { get; set; }
        public int? NoOfInstallment { get; set; }
        public decimal? MonthlyFeeInstallment { get; set; }
        public string? Status { get; set; }
        public bool? IsReturn { get; set; }

        public string? CarInfo { get; set; }

    }
}
