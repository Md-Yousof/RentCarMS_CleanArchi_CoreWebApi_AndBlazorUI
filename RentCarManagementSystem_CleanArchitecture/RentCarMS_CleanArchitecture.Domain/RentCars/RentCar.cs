using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.DuePayments;
using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Domain.Payments;
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.RentCars
{
    public class RentCar
    {
        [Key]
        public int RentCarID { get; set; }
        public int MemberID { get; set; }
        public Member? Member { get; set; }
        public DateTime RentCarDate { get; set; } = DateTime.Now;
        public List<RentCarDetail>? RentCarDetails { get; set; }

        [JsonIgnore]
        public ICollection<Payment>? Payments { get; set; }

        [JsonIgnore]
        public ICollection<DuePayment>? DuePayments { get; set; }
       
    }
}