using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.RentCars
{
    public interface IRentCar
    {
        Task<IEnumerable<RentCar>> GetAllAsynce();
        Task<RentCar> GetByIdAsynce(int id);
        Task CreateAsynce(RentCar content);
        Task UpdateAsync(RentCar rentCar);
        Task DeleteAsynce(int id);

        Task<IEnumerable<RentCar>> GetAllRentCarsWithUpdatedInstallmentsAsync();

        Task<List<RentCar>> GetRentCarsWithMemberInfo();
    }
}
