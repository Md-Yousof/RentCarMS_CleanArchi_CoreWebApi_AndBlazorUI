
using RentCarMS_CleanArchitecture.Domain.RentCarDetails;
using RentCarMS_CleanArchitecture.Domain.RentCars;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Domain.Members
{
    public class Member
    {
        [Key]
        public int MemberID { get; set; }
        public string? FastName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? JoinDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<RentCar>? RentCars { get; set; }

        //[JsonIgnore]
        //public ICollection<Payment>? Payments { get; set; }
        ////public List<Payment>? Payments { get; set; }
    }
}
