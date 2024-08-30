using RentCarMS__BlazorUI.Data.Models;

namespace RentCarMS__BlazorUI.Services.Cars
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllAsynce();
        Task<Car> GetByIdAsynce(int id);
        Task CreateAsynce(Car car);
        Task UpdateAsynce(Car car);
        Task DeleteAsynce(int id);

        Task<List<Car>> GetByCarStatuseTrue();

    }
}
