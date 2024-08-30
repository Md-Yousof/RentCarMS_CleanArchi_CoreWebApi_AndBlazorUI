namespace RentCarMS__BlazorUI.Data.Models
{
    public class RentCar
    {
        public int RentCarID { get; set; }
        public int MemberID { get; set; }
        public DateTime RentCarDate { get; set; }
        public List<RentCarDetail>? RentCarDetails { get; set; } = new List<RentCarDetail>();
        public string? FastName { get; set; } // extra for show FN instead of MemberID
        public string? Model { get; set; }  // extra for show FN instead of MemberID
        public string? MemberInfo { get; set; }  // extra for show FN instead of RentCarId
    }
}
