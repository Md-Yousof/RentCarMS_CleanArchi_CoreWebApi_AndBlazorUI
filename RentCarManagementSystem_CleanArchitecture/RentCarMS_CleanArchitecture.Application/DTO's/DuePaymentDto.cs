using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.DTO_s
{
    public class DuePaymentDto
    {
        public int DuePaymentID { get; set; }
        public int RentCarID { get; set; }
        public int RentCarDetailID { get; set; }
        public DateTime DueDate { get; set; }
        public int DueInstallment { get; set; }
        public decimal DueAmount { get; set; }
    }
}
