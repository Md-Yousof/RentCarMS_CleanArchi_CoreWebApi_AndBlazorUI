using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.DuePayments
{
    public class DuePayment
    {
        [Key]
        public int DuePaymentID { get; set; }
        public int RentCarID { get; set; }
        [JsonIgnore]
        public RentCar? RentCar { get; set; }
        public int RentCarDetailID { get; set; }
        [JsonIgnore]
        public RentCarDetail? RentCarDetail { get; set; }
        public DateTime DueDate { get; set; }
        public int DueInstallment {  get; set; } 
        public decimal DueAmount { get; set; }
        
    }
}
