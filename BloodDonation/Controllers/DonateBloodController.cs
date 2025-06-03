using BloodDonation.DTOs.DonatBloodDTOs;
using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.Models;
using BloodDonation.Services.DonateBloodServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonation.Controllers
{
    [Authorize (Roles = "Donor")]
    [Route("api/[controller]")]
    [ApiController]
    public class DonateBloodController : ControllerBase
    {
        private readonly IDonateBloodService donateBloodService;

        public DonateBloodController(IDonateBloodService donateBloodService)
        {
            this.donateBloodService = donateBloodService;
        }
        // GET: api/AllDonationBloodOfUser
        [HttpGet("GetAllDonationsByUser")]
        public async Task<ActionResult<List<DonateBloodDTO>>> GetAllDonationsByUser()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");

            var donations = await donateBloodService.GetAllBlooodDonationsOfUser(id);
            if (donations == null || donations.Count == 0)
                return NotFound("No donations found for this user.");
            return Ok(donations);
        }

        [AllowAnonymous]
        // GET: api/DonateBlood/GetAllDonationsByHospital/{id}
        [HttpGet("GetAllDonationsByHospital/{id}")]
        public async Task<ActionResult<List<DonateBloodDTO>>> GetAllDonationsByHospital(string id)
        {
            var donations = await donateBloodService.GetAllBlooodDonationsOfHospital(id);
            if (donations == null || donations.Count == 0)
                return NotFound("No donations found for this hospital.");
            return Ok(donations);
        }

        // GET: api/DonateBlood/donor
        [HttpGet("GetDonor")]
        public async Task<ActionResult<DonorDTO>> GetDonor()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");

            var donor = await donateBloodService.GetDonorByID(id);
            if (donor == null)
                return NotFound("Donor not found.");
            return Ok(donor);
        }

        // POST: api/DonateBlood
        [HttpPost("Donate")]
        public async Task<ActionResult<DonateBloodDTO>> Donate( [FromBody] DonateBloodDTO dto)
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");
            dto.DonorID = id;
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await donateBloodService.Donate(dto);
            if (result == null)
                return BadRequest("Donation failed. Check donor or hospital existence, or donation amount.");
            return Ok(result);
        }

        // PUT: api/DonateBlood
        [HttpPut("EditDonation")]
        public async Task<ActionResult<DonateBloodDTO>> EditDonation(int DonateId, [FromBody] DonateBloodDTO dto)
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");
            dto.DonorID = id;
            dto.Id = DonateId;
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await donateBloodService.EditDonate(DonateId, dto);
            if (result == null)
                return NotFound("Donation to update not found or invalid.");
            return Ok(result);
        }

        // DELETE: api/DonateBlood/{id}
        [HttpDelete("DeleteDonation/{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            int result = await donateBloodService.DeleteDonate(id);
            if (result == -1) return NotFound("Donation not found.");
            if (result == 0) return BadRequest("Donation already deleted.");
            return Ok("Donation deleted successfully.");
        }
    }
}