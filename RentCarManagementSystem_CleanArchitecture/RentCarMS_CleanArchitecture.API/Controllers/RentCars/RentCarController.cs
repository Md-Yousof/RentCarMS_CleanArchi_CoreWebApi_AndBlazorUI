using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Application.RentCars.RentCarServices;

namespace RentCarMS_CleanArchitecture.API.Controllers.RentCars
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentCarController : ControllerBase
    {
        private readonly RentCarService _rentCarService;

        public RentCarController(RentCarService rentCarService)
        {
            _rentCarService = rentCarService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRentCar(/*[FromBody]*/ RentCarDTO rentCarDto)
        {
            if (rentCarDto == null)
            {
                return BadRequest("RentCarDTO is null.");
            }

            var result = await _rentCarService.CreateRentCarAsync(rentCarDto);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rentCars = await _rentCarService.GetAllRentCarsAsync();
            return Ok(rentCars);
        }

        [HttpGet("GetAllDueNofInstallment")]
        public async Task<IActionResult> GetAllUpdateNofInstallment()
        {
            var rentCars = await _rentCarService.GetAllRentCarsWithUpdatedInstallmentsAsync();
            return Ok(rentCars);
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentCarById(int id)
        {
            var rentCar = await _rentCarService.GetRentCarById(id);

            if (rentCar == null)
            {
                return NotFound();
            }

            return Ok(rentCar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> put(int id, /*[FromBody]*/ RentCarDTO rentCarDto)
        {
            if (id != rentCarDto.RentCarID)
            {
                return BadRequest("ID mismatch.");
            }

            var updatedRentCar = await _rentCarService.UpdateRentCarAsync(rentCarDto);

            if (updatedRentCar == null)
            {
                return NotFound();
            }
            return Ok(new { Message = "Update successful." });
            // return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rentCarService.DeleteRentCar(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(new { massage = "Delete rentCar Successsfully" });
        }


        [HttpGet("WithMemberInfo")]
        public async Task<ActionResult<List<RentCarDTO>>> GetRentCarsWithMemberInfo()
        {
            var rentCars = await _rentCarService.GetRentCarsWithMemberInfo();

            if (rentCars == null || !rentCars.Any())
            {
                return NotFound("No RentCar records found.");
            }

            return Ok(rentCars);
        }
    }
}
