using Microsoft.Extensions.Hosting;
using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Domain.Cars;
using RentCarMS_CleanArchitecture.Domain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.Cars.CarServices
{
    public class CareService
    {
        public readonly ICarRepository _repository;
        public CareService(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CarDTO>> GetAllCar()
        {
            var car = await _repository.GetAllAsync();
            return car.Select(c => new CarDTO
            {
                CarID = c.CarID,
                LicensePlaete = c.LicensePlaete,
                Model = c.Model,
                Quentity = c.Quentity,
                Status = c.Status,
            }).ToList();
        }

        public async Task<CarDTO> GetCarById(int id)
        {
            var cars = await _repository.GetByIdAsync(id);
            return new CarDTO
            {
               
                CarID = cars.CarID,
                LicensePlaete = cars.LicensePlaete,
                Model = cars.Model,
                Quentity = cars.Quentity,
                Status = cars.Status,

            };
        }
        public async Task<List<CarDTO>> GetByCarStatusTrueAsync()
        {
            var statuses = await _repository.GetByStatusTrue();
            return statuses.Select(c => new CarDTO
            {
                CarID = c.CarID,
                LicensePlaete = c.LicensePlaete,
                Model = c.Model,
                Quentity = c.Quentity,
                Status = c.Status,
            }).ToList();
        }

        public async Task<CarDTO> CreateCarAsync(CarDTO carDTO)
        {
            
            var car = new Car
            {                
                LicensePlaete = carDTO.LicensePlaete,
                Model = carDTO.Model,
                //Quentity = carDTO.Quentity,
                Quentity = 1,
                Status = true,
                //Status = carDTO.Status
            };

            var creatCar = await _repository.CreateAsync(car);
            carDTO.CarID = creatCar.CarID;
            return carDTO;

        }

        public async Task<CarDTO> UpdateCarAsync(CarDTO carDTO)
        {
            var existingEntity = await _repository.GetByIdAsync(carDTO.CarID);
            //var existingEntity = await GetMemberById(member.MemberID);
            if (existingEntity == null)
            {
                return null;
            }
            existingEntity.LicensePlaete = carDTO.LicensePlaete;
            existingEntity.Model = carDTO.Model;
            existingEntity.Quentity = carDTO.Quentity;
            existingEntity.Status = carDTO.Status;
          
            await _repository.UpdateAsync(existingEntity);          
            return carDTO;

        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}