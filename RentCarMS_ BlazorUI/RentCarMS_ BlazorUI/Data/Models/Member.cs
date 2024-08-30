using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RentCarMS__BlazorUI.Data.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string? FastName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? JoinDate { get; set; } 
        public IBrowserFile? ImageFile { get; set; }

      
    }
}
