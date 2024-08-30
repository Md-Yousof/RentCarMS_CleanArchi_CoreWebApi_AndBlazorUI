using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.Cars
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }
        public string? LicensePlaete { get; set; }
        public string? Model { get; set; }
        public int? Quentity { get; set; }
        public bool? Status { get; set; }

        [JsonIgnore]
        public ICollection<RentCarDetail>? RentCarDetails { get; set; }






        //public void UpdateStatus()
        //{
        //    if (Quentity.HasValue)
        //    {
        //        Status = Quentity.Value > 0;
        //    }
        //}
    }
}
