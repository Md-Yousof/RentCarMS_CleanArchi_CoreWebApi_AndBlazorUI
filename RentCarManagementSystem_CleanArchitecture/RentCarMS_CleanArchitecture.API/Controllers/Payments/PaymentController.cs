using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCarMS_CleanArchitecture.Application.Cars.CarServices;
using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Application.Payments.PaymentServices;

namespace RentCarMS_CleanArchitecture.API.Controllers.Payments
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pary = await _paymentService.GetAllPayment();
            if (pary == null)
            {
                throw new ArgumentException("Payments Cannot found or not Available here bojecho..");
            }
            return Ok(pary);
        }

        [HttpPost]
        public async Task<IActionResult> Post(/*[FromBody]*/ PaymentDto payDto)
        {
            var create = await _paymentService.CreatePayment(payDto);
            if (create == null)
            {
                return NotFound();
            }
            return Ok(new { Massage = "Data Successfully post" });
        }

        [HttpGet("GetMemberInfoInListPaymentByRentCarId")]
        public async Task<ActionResult<List<RentCarDTO>>> GetMemberInfoInListPaymentByRentCarId()
        {
            var rentCars = await _paymentService.GetMemberInfoInListPaymentByRentCarId();

            if (rentCars == null || !rentCars.Any())
            {
                return NotFound("No RentCar records found.");
            }

            return Ok(rentCars);
        }
    }
}
