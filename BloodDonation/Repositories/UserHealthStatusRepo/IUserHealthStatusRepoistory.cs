using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;

namespace BloodDonation.Repositories.UserHealthStatusRepo
{
    public interface IUserHealthStatusRepoistory : IRepository<UserHealthStatus>
    {
        public Task<List<UserHealthStatus>> GetAllUserHealthStatus(string id);
        public Task<UserHealthStatus> GetUserHealthStatusByID(int id);
        public Task<UserHealthStatus> AddUserHealthStatus(UserHealthStatus UserHealthStatus);
        public Task<int> CalculateScore(UserHealthStatus userHealthStatus);
        public Task<int> DeleteUserHealthStatus(int UserhealthStatusId);

    }
}