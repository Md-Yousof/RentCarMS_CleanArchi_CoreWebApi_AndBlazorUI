using RentCarMS__BlazorUI.Data.Models;
using RentCarMS__BlazorUI.Services.Payments;

namespace RentCarMS__BlazorUI.Services.RentCarDetails
{
    public class RentCarDetailService: IRenCareDetailService
    {
        private readonly HttpClient httpClient;
        public RentCarDetailService(HttpClient httpClient)
        {
             this.httpClient = httpClient;
        }

        public async Task<IEnumerable<RentCarDetail>> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<RentCarDetail[]>("api/RentCarDetail");
        }

        public async Task<List<RentCarDetail>> GetCarInfoListForCreatePayment()
        {
            return await httpClient.GetFromJsonAsync<List<RentCarDetail>>("api/RentCarDetail/CarInfo");
        }

        public async Task<List<RentCarDetail>> GetIsReturnFalse()
        {
            return await httpClient.GetFromJsonAsync<List<RentCarDetail>>("api/RentCarDetail/IsReturnFalse");
        }

        public async Task<RentCarDetail> GetRentCarDetailsByRentCarIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<RentCarDetail>($"api/RentCarDetail/GetbyIdDetals/{id}");
        }


        //public async Task<IEnumerable<RentCarDetail>> GetRentCarStatusAsync()
        //{
        //    return await httpClient.GetFromJsonAsync<List<RentCarDetail>>("api/RentCarDetail/GetByStatue");
        //}
    }
}
