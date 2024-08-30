using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.DuePayments
{
    public class DuePaymentService: IDuePayment
    {
        private readonly HttpClient httpClient;
        public DuePaymentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<DuePayment>> GetAllAsynce()
        {
            return await httpClient.GetFromJsonAsync<DuePayment[]>("api/DuePayment");
        }
    }
}
