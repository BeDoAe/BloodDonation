using BloodDonation.DTOs.UserHealthStatusDTOs;
using BloodDonation.Models;
using BloodDonation.Services.UserHealthStatusServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonation.Controllers
{
    [Authorize(Policy = "DonorOrRecipient")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestController : ControllerBase
    {
        private readonly IUserHealthStatusService userHealthStatusService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserTestController(IUserHealthStatusService userHealthStatusService , UserManager<ApplicationUser> userManager)
        {
            this.userHealthStatusService = userHealthStatusService;
            this.userManager = userManager;
        }
        // GET: api/UserTest
        [HttpGet]
        public async Task<ActionResult<List<UserHealthStatusDTO>>> GetAllUserHealthStatuses()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");
            
            var result = await userHealthStatusService.GetAllUserHealthStatuses(id);
            return Ok(result);
        }

        // GET: api/UserTest/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserHealthStatusDTO>> GetUserHealthStatusById(int id)
        {
            var result = await userHealthStatusService.GetUserHealthStatusById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/UserTest
        [HttpPost]
        public async Task<ActionResult<UserHealthStatusDTO>> AddUserHealthStatus([FromBody] UserHealthStatusDTO model)
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");
            model.UserId = id;
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await userHealthStatusService.AddUserHealthStatus(model);
            if (result == null) return BadRequest("Invalid or missing answers.");
            return Ok(result);
        }

        // DELETE: api/UserTest/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserHealthStatus(int id)
        {
            var result = await userHealthStatusService.DeleteUserHealthStatus(id);
            if (result==-1)
            {
                return NotFound("User health status not found.");
            }else if (result==0)
            {
                return BadRequest("Already deleted.");
            }
            else
            {
                return Ok("Deleted successfully.");
            }
  
        }

        // POST: api/UserTest/calculate-score
        [HttpPost("calculateScore/{id}")]
        public async Task<ActionResult<int>> CalculateScore(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            int score = await userHealthStatusService.CalculateScore(id);
            if (score == -1) return BadRequest("Invalid user or health status.");
            return Ok(score);
        }
    }
}