namespace RentCarMS__BlazorUI.Data.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int RentCarID { get; set; }
        public int RentCarDetailID { get; set; }
        public int? NofInstallMent { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal PaidAmmount { get; set; }
        public int? AdvanceInstallMent { get; set; }

        public string? MemberInfo { get; set; }  // extra for show FN instead of RentCarId
        public string? CarInfo { get; set; } // extra for show FN instead of RentCarId
        // public List<RentCar>? RentCars { get; set; } = new List<RentCar>();

    }
}
