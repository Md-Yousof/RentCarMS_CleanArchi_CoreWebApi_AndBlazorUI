using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.Cars
{
    public class CarService: ICarService
    {
        private readonly HttpClient httpClient;
        public CarService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Car>> GetAllAsynce()
        {
          return  await httpClient.GetFromJsonAsync<Car[]>("api/Car");
        }

        public async Task<Car> GetByIdAsynce(int id)
        {
            return await httpClient.GetFromJsonAsync<Car>($"api/Car/{id}");
        }
        public async Task CreateAsynce(Car car)
        {
            await httpClient.PostAsJsonAsync("api/Car",car);
        }

        public async Task UpdateAsynce(Car car)
        {
            var respon = await httpClient.PutAsJsonAsync($"api/Car/{car.CarID}", car);
            if (respon.IsSuccessStatusCode)
            {
                Console.WriteLine("Car updated successfully.");
            }
            else
            {
                // If the response is not successful, handle it accordingly
                var errorMessage = await respon.Content.ReadAsStringAsync();
                throw new Exception($"Failed to update car. Status Code: {respon.StatusCode}. Error: {errorMessage}");
            }

        }
        public async Task DeleteAsynce(int id)
        {
            await httpClient.DeleteAsync($"api/Car/{id}");
        }

        public async Task<List<Car>> GetByCarStatuseTrue()
        {
            return await httpClient.GetFromJsonAsync<List<Car>>("api/Car/GetByStatus");

        }
    }
}
