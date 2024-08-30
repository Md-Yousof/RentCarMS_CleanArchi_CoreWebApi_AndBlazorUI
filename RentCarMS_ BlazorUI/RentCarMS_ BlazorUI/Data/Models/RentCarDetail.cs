using Microsoft.AspNetCore.Routing.Matching;

namespace RentCarMS__BlazorUI.Data.Models
{
    public class RentCarDetail
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
        public string? Model { get; set; } //extra for Get Model Name in the RentCarList page instead Of CarId
        public string? CarInfo { get; set; }   // extra for show FN instead of RentCarDetailId

    }
}
