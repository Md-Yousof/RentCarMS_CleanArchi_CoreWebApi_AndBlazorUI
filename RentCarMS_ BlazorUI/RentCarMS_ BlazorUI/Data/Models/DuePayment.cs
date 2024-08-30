namespace RentCarMS__BlazorUI.Data.Models
{
    public class DuePayment
    {
        public int DuePaymentID { get; set; }
        public int RentCarID { get; set; }
        public int RentCarDetailID { get; set; }
        public DateTime DueDate { get; set; }
        public int DueInstallment { get; set; }
        public decimal DueAmount { get; set; }


       // public string? MemberInfo { get; set; }  // extra for show FN instead of RentCarId
        //public string? CarInfo { get; set; } // extra for show FN instead of RentCarId
    }
}
