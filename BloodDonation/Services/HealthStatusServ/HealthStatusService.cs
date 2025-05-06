using AutoMapper;
using BloodDonation.DTOs.HealthStatusDTOs;
using BloodDonation.Models;
using BloodDonation.Repositories.HealthStatusRepo;
using BloodDonation.Services.GenericServ;

namespace BloodDonation.Services.HealthStatusServ
{
    public class HealthStatusService : Service<HealthStatus> , IHealthStatusService
    {
        private readonly IHealthStatusRepository healthStatusRepository;
        private readonly IMapper automapper;

        public HealthStatusService(IHealthStatusRepository healthStatusRepository , IMapper automapper)
        {
            this.healthStatusRepository = healthStatusRepository;
            this.automapper = automapper;
        }
        public async Task<List<HealthStatusDTO>> GetAllAsync()
        {
            var statuses = await healthStatusRepository.GetAllHealthStatus();
            return automapper.Map<List<HealthStatusDTO>>(statuses);
        }

        public async Task<HealthStatusDTO> GetByIdAsync(int id)
        {
            var status = await healthStatusRepository.GetHealthStatusByID(id);
            if (status == null) return null;
            return  automapper.Map<HealthStatusDTO>(status);
        }

        public async Task<HealthStatusDTO> CreateAsync(HealthStatusDTO dto)
        {
            var model = automapper.Map<HealthStatus>(dto);
            var createdStatus = await healthStatusRepository.AddHealthStatus(model);
            if (createdStatus == null) return null;
            return  automapper.Map<HealthStatusDTO>(createdStatus);
        }

        public async Task<HealthStatusDTO> UpdateAsync(int id, HealthStatusDTO dto)
        {
            dto.Id = id;
            var model = automapper.Map<HealthStatus>(dto);
            var updatedStatus = await healthStatusRepository.UpdateHealthStatus(model);
            if (updatedStatus == null) return null;
            return automapper.Map<HealthStatusDTO>(updatedStatus);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var result = await healthStatusRepository.DeleteHealthStatus(id);
            return result ;
        }

        public async Task<int> DeleteQuestionAsync(int questionId)
        {
            var result = await healthStatusRepository.DeleteHealthQuestion(questionId);
            return result;
        }
    }
}