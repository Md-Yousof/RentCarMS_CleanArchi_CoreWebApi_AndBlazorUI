using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.Payments
{
   
    public class PaymentService:IPaymentService
    {
        private readonly HttpClient httpClient;
        public PaymentService(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
        }

        public async Task CreatePayment(Payment payment)
        {
            await httpClient.PostAsJsonAsync("api/Payment", payment);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
           return await httpClient.GetFromJsonAsync<Payment[]>("api/Payment");
        }

        public async Task<List<Payment>> GetMemberInfoInListPaymentByRentCarId()
        {
            return await httpClient.GetFromJsonAsync<List<Payment>>("api/Payment/GetMemberInfoInListPaymentByRentCarId");
        }
    }
}
