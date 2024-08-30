using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.DTO_s
{
    public class RentCarDTO
    {
        public int RentCarID { get; set; }
        public int MemberID { get; set; }
        public DateTime RentCarDate { get; set; }
        public List<RentCarDetailDTO>? RentCarDetails { get; set; }

        public string? MemberInfo { get; set; } //extra niyeci

        // public ICollection<PaymentDto>? Payments { get; set; }
    }
}
