using BloodDonation.Models;

namespace BloodDonation.Repositories.HealthStatusRepo
{
    public interface IHealthStatusRepository
    {
        public Task<List<HealthStatus>> GetAllHealthStatus();
        public Task<HealthStatus> GetHealthStatusByID(int id);

        public Task<HealthStatus> AddHealthStatus(HealthStatus healthStatus);
        public Task<List<HealthQuestion>> AddHealthQuestions(int healthStatusId, List<HealthQuestion> questions);
        public Task<HealthStatus> UpdateHealthStatus( HealthStatus updatedStatus);
        public Task<int> DeleteHealthStatus(int healthStatusId);
        public Task<int> DeleteHealthQuestion(int questionId);

    }
}