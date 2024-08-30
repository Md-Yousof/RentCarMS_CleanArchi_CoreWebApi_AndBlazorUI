using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RentCarMS_CleanArchitecture.Application.DuePayments.DuePaymentServices;
using RentCarMS_CleanArchitecture.Domain.DuePayments;

namespace RentCarMS_CleanArchitecture.API.Controllers.DuePayments
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuePaymentController : ControllerBase
    {
        private readonly DuePaymentService _duePaymentRe;
        public DuePaymentController(DuePaymentService duePaymentRe)
        {
            _duePaymentRe = duePaymentRe;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var duepay = await _duePaymentRe.GetDuePayments();
            return Ok(duepay);
        }

        
    }
}
