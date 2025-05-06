using BloodDonation.DTOs.UserHealthStatusDTOs;
using BloodDonation.Models;
using BloodDonation.Services.GenericServ;

namespace BloodDonation.Services.UserHealthStatusServ
{
    public interface IUserHealthStatusService : IService<UserHealthStatus>
    {
        public Task<List<UserHealthStatusDTO>> GetAllUserHealthStatuses(string id);
        public Task<UserHealthStatusDTO> GetUserHealthStatusById(int id);
        public Task<UserHealthStatusDTO> AddUserHealthStatus(UserHealthStatusDTO userHealthStatusDTO);
        public Task<int> DeleteUserHealthStatus(int id);
        public Task<int> CalculateScore(int userHealthStatusID);

    }
}