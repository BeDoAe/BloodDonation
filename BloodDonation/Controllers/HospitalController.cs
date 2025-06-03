using BloodDonation.Services.HospitalServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonation.Controllers
{
    [Authorize(Roles = "Hospital")]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalService hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            this.hospitalService = hospitalService;
        }
        // GET: api/Hospital/GetAllHospitals
        [HttpGet("GetAllHospitals")]
        public async Task<IActionResult> GetAllHospitals()
        {
            var hospitals = await hospitalService.GetAllHospitals();
            return Ok(hospitals);
        }

        // GET: api/Hospital/GetHospitalById
        [HttpGet("GetHospitalById/{id}")]
        public async Task<IActionResult> GetHospitalById()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");

            var hospital = await hospitalService.GetHospitalById(id);
            return Ok(hospital);
        }

        // GET: api/Hospital/GetAllDonorsOfHospital
        [HttpGet("GetAllDonorsOfHospital")]
        public async Task<IActionResult> GetAllDonorsOfHospital()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");
            var donors = await hospitalService.GetAllDonorsOfHospital(id);
            return Ok(donors);
        }

        // GET: api/Hospital/GetAllReciepentsOfHospital/
        [HttpGet("GetAllReciepentsOfHospital")]
        public async Task<IActionResult> GetAllReciepentsOfHospital()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");

            var recipients = await hospitalService.GetAllReciepentsOfHospital(id);
            return Ok(recipients);
        }

        // GET: api/Hospital/GetAllBloodDonationsOfHospital
        [HttpGet("GetAllBloodDonationsOfHospital")]
        public async Task<IActionResult> GetAllBloodDonationsOfHospital()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");

            var donations = await hospitalService.GetAllBloodDonationsOfHospital(id);
            return Ok(donations);
        }

        // GET: api/Hospital/GetAvailableBloodAmount/
        [HttpGet("GetAvailableBloodAmount")]
        public async Task<IActionResult> GetAvailableBloodAmount()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(id))
                return Unauthorized("User ID not found in token.");

            var amount = await hospitalService.GetAvailableBloodAmount(id);
            return Ok(amount);
        }

    }
}
