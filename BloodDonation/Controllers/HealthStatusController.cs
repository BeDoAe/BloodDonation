using BloodDonation.DTOs.HealthStatusDTOs;
using BloodDonation.Services.HealthStatusServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.Controllers
{
    [Authorize(Roles = "Hospital")]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthStatusController : ControllerBase
    {
        private readonly IHealthStatusService _healthStatusService;

        public HealthStatusController(IHealthStatusService healthStatusService)
        {
            _healthStatusService = healthStatusService;
        }

        // 1) Get all health statuses
        [HttpGet]
        public async Task<ActionResult<List<HealthStatusDTO>>> GetAll()
        {
            var statuses = await _healthStatusService.GetAllAsync();
            if (statuses == null || statuses.Count == 0) return NotFound("No health statuses found.");
            return Ok(statuses);
        }

        // 2) Get health status by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthStatusDTO>> GetById(int id)
        {
            var status = await _healthStatusService.GetByIdAsync(id);
            if (status == null) return NotFound($"Health status with ID {id} not found.");
            return Ok(status);
        }

        // 3) Create new health status
        [HttpPost]
        public async Task<ActionResult<HealthStatusDTO>> Create([FromBody] HealthStatusDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdStatus = await _healthStatusService.CreateAsync(dto);
            if (createdStatus == null) return BadRequest("Failed to create health status.");

            return CreatedAtAction(nameof(GetById), new { id = createdStatus.Id }, createdStatus);
        }

        // 4) Update health status
        [HttpPut("{id}")]
        public async Task<ActionResult<HealthStatusDTO>> Update(int id, [FromBody] HealthStatusDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedStatus = await _healthStatusService.UpdateAsync(id, dto);
            if (updatedStatus == null) return NotFound($"Health status with ID {id} not found.");

            return Ok(updatedStatus);
        }

        // 5) Delete health status
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _healthStatusService.DeleteAsync(id);
            if (result == -1) return NotFound($"Health status with ID {id} not found.");
            if (result == 0) return BadRequest("Health status is already deleted.");

            return Ok("Health status deleted successfully.");
        }

        // 6) Delete health question
        [HttpDelete("question/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            var result = await _healthStatusService.DeleteQuestionAsync(questionId);
            if (result == -1) return NotFound($"Health question with ID {questionId} not found.");
            if (result == 0) return BadRequest("Health question is already deleted.");

            return Ok("Health question deleted successfully.");
        }
    }
}
