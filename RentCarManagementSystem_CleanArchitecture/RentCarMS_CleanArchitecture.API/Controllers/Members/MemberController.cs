
using Microsoft.AspNetCore.Mvc;
using RentCarMS_CleanArchitecture.Application.Members.Service;
using RentCarMS_CleanArchitecture.Domain.Members;
using RentCarMS_CleanArchitecture.Application.Members;
using RentCarMS_CleanArchitecture.Application.DTO_s;

namespace RentCarMS_CleanArchitecture.API.Controllers.Members
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberService _memberService;
        public MemberController(MemberService member)
        {
            _memberService = member;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var mem = await _memberService.GetAllMember();
            return Ok(mem);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var mem = await _memberService.GetMemberById(id);
            if (mem == null)
            {
                return NotFound();
            }
            return Ok(mem);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MemberDTO m)
        {
            var mem = await _memberService.CreateMemberAsync(m);
            if (mem == null)
            {
                return NotFound();
            }
            return Ok(new { Massage = "Data Successfully post" });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] MemberDTO m)
        {
            var mem = await _memberService.UpdateMemberAsync(m);
            if (mem == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to Update Member");
            }
            
            return Ok(new { Massage = "Data Successfully Update" });
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> Delete(int id)
        {
            var mem = await _memberService.DeleteMemberAsync(id);
            if (!mem)
            {
                return NotFound();
            }
            return Ok(new { Massage = "Data Successfully Delete" });
        }
    }
}
