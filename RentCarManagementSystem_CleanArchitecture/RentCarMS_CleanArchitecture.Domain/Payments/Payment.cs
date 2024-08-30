using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.Payments
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public int RentCarID { get; set; }
        [JsonIgnore]
        public RentCar? RentCar { get; set; }
        public int RentCarDetailID { get; set; }
        public int? NofInstallMent { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal PaidAmmount { get; set; }
        public int? AdvanceInstallMent { get; set; }
       

        //public string? PaymentStatuse { get; set; }

    }
}
