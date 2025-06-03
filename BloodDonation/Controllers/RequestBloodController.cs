using BloodDonation.DTOs.DonatBloodDTOs;
using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.DTOs.ReciepentDTOs;
using BloodDonation.DTOs.RequestBloodDTOs;
using BloodDonation.Models;
using BloodDonation.Services.DonateBloodServ;
using BloodDonation.Services.RequestBloodServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonation.Controllers
{
    [Authorize(Roles = "Recipient")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestBloodController : ControllerBase
    {
        private readonly IRequestBloodService requestBloodService;

        public RequestBloodController(IRequestBloodService requestBloodService)
        {
            this.requestBloodService = requestBloodService;
        }
        // GET: api/RequestBlood/GetAllRequestsByUser
        [HttpGet("GetAllRequestsByUser")]
        public async Task<ActionResult<List<RequestBloodDTO>>> GetAllRequestsByUser()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            var requests = await requestBloodService.GetAllRequestsByUser(userId);
            if (requests == null || requests.Count == 0)
                return NotFound("No blood requests found for this user.");
            return Ok(requests);
        }

        // GET: api/RequestBlood/GetAllRequestsByHospital/{id}
        [AllowAnonymous]
        [HttpGet("GetAllRequestsByHospital/{id}")]
        public async Task<ActionResult<List<RequestBloodDTO>>> GetAllRequestsByHospital(string id)
        {
            var requests = await requestBloodService.GetAllRequestsByHospital(id);
            if (requests == null || requests.Count == 0)
                return NotFound("No blood requests found for this hospital.");
            return Ok(requests);
        }

        // GET: api/RequestBlood/GetRecipient
        [HttpGet("GetRecipient")]
        public async Task<ActionResult<RecpientDTO>> GetRecipient()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            var recipient = await requestBloodService.GetRecpientByID(userId);
            if (recipient == null)
                return NotFound("Recipient not found.");
            return Ok(recipient);
        }

        // POST: api/RequestBlood/Request
        [HttpPost("Request")]
        public async Task<ActionResult<RequestBloodDTO>> RequestBlood([FromBody] RequestBloodDTO dto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            dto.ReciepentID = userId;
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await requestBloodService.Request(dto);
            if (result == null)
                return BadRequest("Blood request failed.");
            return Ok(result);
        }

        // PUT: api/RequestBlood/EditRequest/{id}
        [HttpPut("EditRequest/{id}")]
        public async Task<ActionResult<RequestBloodDTO>> EditRequest(int id, [FromBody] RequestBloodDTO dto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            dto.ReciepentID = userId;
            dto.Id = id;
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await requestBloodService.EditRequest(dto);
            if (result == null)
                return NotFound("Request to update not found or invalid.");
            return Ok(result);
        }

        // DELETE: api/RequestBlood/DeleteRequest/{id}
        [HttpDelete("DeleteRequest/{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            int result = await requestBloodService.DeleteRequest(id);
            if (result == -1) return NotFound("Request not found.");
            if (result == 0) return BadRequest("Request already deleted.");
            return Ok("Request deleted successfully.");
        }
    }
}