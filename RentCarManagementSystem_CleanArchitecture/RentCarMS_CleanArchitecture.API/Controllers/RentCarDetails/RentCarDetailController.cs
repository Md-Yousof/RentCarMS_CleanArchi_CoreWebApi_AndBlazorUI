using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Application.RentCarDetails.RentCarDetailServices;
using RentCarMS_CleanArchitecture.Infrastructure.DATA.Context;

namespace RentCarMS_CleanArchitecture.API.Controllers.RentCarDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentCarDetailController : ControllerBase
    {
        private readonly RentCarDetailService _service;
        public RentCarDetailController(RentCarDetailService service)
        {

            _service = service;
        }

        [HttpGet("IsReturnFalse")]
        public async Task<IActionResult> Get()
        {
            var isretun = (await _service.GetAllIsReturnFalse()).ToList();
            return Ok(isretun);
        }

        [HttpGet("{GetbyIdDetals}")]
        public async Task<IActionResult> Get(int id)
        {
            var mem = await _service.GetRentCarDetailsByRentCarIdAsync(id);
            if (mem == null)
            {
                return NotFound();
            }
            return Ok(mem);
        }

        [HttpGet("CarInfo")]
        public async Task<ActionResult<List<RentCarDetailDTO>>> GetCarInfoListForCreatePayment()
        {
            var carInfo = await _service.GetCarInfoListForCreatePayment();
            if (carInfo == null || !carInfo.Any())
            {
                return NotFound("No Car records found.");
            }
            return Ok(carInfo);

        }
    }
}
