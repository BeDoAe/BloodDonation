using BloodDonation.DTOs.HealthStatusDTOs;
using BloodDonation.Models;
using BloodDonation.Services.GenericServ;

namespace BloodDonation.Services.HealthStatusServ
{
    public interface IHealthStatusService :IService<HealthStatus>
    {
        public Task<List<HealthStatusDTO>> GetAllAsync();
        public Task<HealthStatusDTO> GetByIdAsync(int id);
        public Task<HealthStatusDTO> CreateAsync(HealthStatusDTO dto);
        public Task<HealthStatusDTO> UpdateAsync(int id, HealthStatusDTO dto);
        public Task<int> DeleteAsync(int id);
        public Task<int> DeleteQuestionAsync(int questionId);

    }
}