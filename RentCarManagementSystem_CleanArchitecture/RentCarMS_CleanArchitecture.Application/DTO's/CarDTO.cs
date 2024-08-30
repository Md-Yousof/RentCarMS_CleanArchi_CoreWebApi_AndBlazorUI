using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.DTO_s
{
    public class CarDTO
    {
        public int CarID { get; set; }
        public string? LicensePlaete { get; set; }
        public string? Model { get; set; }
        public int? Quentity { get; set; }
        public bool? Status { get; set; }

    }
}
