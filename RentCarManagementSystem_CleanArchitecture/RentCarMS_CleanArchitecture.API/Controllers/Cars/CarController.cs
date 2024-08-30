using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCarMS_CleanArchitecture.Application.Cars.CarServices;
using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Application.Members.Service;

namespace RentCarMS_CleanArchitecture.API.Controllers.Cars
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CareService _careService;
        public CarController(CareService careService)
        {
            _careService = careService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var mem = await _careService.GetAllCar();
            return Ok(mem);
        }

        [HttpGet("GetByStatus")]
        public async Task<IActionResult> GetByCarStatus()
        {
            var mem = await _careService.GetByCarStatusTrueAsync();
            if (mem == null)
            {
                throw new Exception("Car Not Available here..");

            }
            return Ok(mem);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var mem = await _careService.GetCarById(id);
            if (mem == null)
            {
                return NotFound();
            }
            return Ok(mem);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CarDTO c)
        {
            var car = await _careService.CreateCarAsync(c);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(new { Massage = "Data Successfully post" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CarDTO c)
        {
            var mem = await _careService.UpdateCarAsync(c);
            if (mem == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Update Member");
            }

            return Ok(new { Massage = "Data Successfully Update" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mem = await _careService.DeleteCarAsync(id);
            if (!mem)
            {
                return NotFound();
            }
            return Ok(new { Massage = "Data Successfully Delete" });
        }
    }
}