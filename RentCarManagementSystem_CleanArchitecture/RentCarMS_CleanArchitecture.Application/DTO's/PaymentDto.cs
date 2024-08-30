using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.DTO_s
{
    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public int RentCarID { get; set; }
        //public RentCarDTO? RentCar { get; set; }
        public int RentCarDetailID { get; set; }      
        public int? NofInstallMent { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal PaidAmmount { get; set; }
        public int? AdvanceInstallMent { get; set; }

        public string? MemberInfo { get; set; } //Extra for Get MemberName Instead of Id
        public string? CarInfo { get; set; }  //Extra for Get CarName Instead of Id

    }
}
