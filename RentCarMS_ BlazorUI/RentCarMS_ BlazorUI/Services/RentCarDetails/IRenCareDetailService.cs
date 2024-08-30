using RentCarMS__BlazorUI.Data.Models;
namespace RentCarMS__BlazorUI.Services.RentCarDetails
{
    public interface IRenCareDetailService
    {
        Task<IEnumerable<RentCarDetail>> GetAllAsync();
        Task<List<RentCarDetail>> GetIsReturnFalse();
        Task<RentCarDetail> GetRentCarDetailsByRentCarIdAsync(int id);

        Task<List<RentCarDetail>> GetCarInfoListForCreatePayment();
        // Task<IEnumerable<RentCarDetail>> GetRentCarStatusAsync();
    }
}
