using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.RentCars
{
    public class RentCarService: IRentCar
    {
        private HttpClient _httpClient;

        public RentCarService(HttpClient httpClient) 
        { 
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<RentCar>> GetAllAsynce()
        {
            return await _httpClient.GetFromJsonAsync<RentCar[]>("api/RentCar");
        }

        public async Task<RentCar> GetByIdAsynce(int id)
        {
          return  await _httpClient.GetFromJsonAsync<RentCar>($"/api/RentCar/{id}");
        }
        public async Task CreateAsynce(RentCar content)
        {
          var respose =  await _httpClient.PostAsJsonAsync("/api/RentCar", content);
            if (!respose.IsSuccessStatusCode) 
            {
                throw new Exception("Failed to create RenCar bojecho...");
            }

        }

        public async Task DeleteAsynce(int id)
        {
            await _httpClient.DeleteAsync($"/api/RentCar/{id}");
        }

        public async Task UpdateAsync(RentCar rentCar)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"/api/RentCar/{rentCar.RentCarID}", rentCar);
            }
            catch (Exception e)
            {

                Console.WriteLine($"Rent Car Cannot update bojecho..: {rentCar.RentCarID}, Error massage: {e.Message}");
            }
          

        }

        public async Task<IEnumerable<RentCar>> GetAllRentCarsWithUpdatedInstallmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<RentCar[]>("api/RentCar/GetAllDueNofInstallment");
        }

        public async Task<List<RentCar>> GetRentCarsWithMemberInfo()
        {
            return await _httpClient.GetFromJsonAsync<List<RentCar>>("api/RentCar/WithMemberInfo");
        }
    }
}
