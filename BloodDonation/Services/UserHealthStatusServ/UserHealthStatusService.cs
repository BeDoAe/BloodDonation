    using AutoMapper;
    using BloodDonation.DTOs.UserHealthStatusDTOs;
    using BloodDonation.Models;
    using BloodDonation.Repositories.UserHealthStatusRepo;
    using BloodDonation.Services.GenericServ;

    namespace BloodDonation.Services.UserHealthStatusServ
    {
        public class UserHealthStatusService : Service<UserHealthStatus> , IUserHealthStatusService
        {
            private readonly IUserHealthStatusRepoistory userHealthStatusRepoistory;
            private readonly IMapper automapper;

            public UserHealthStatusService(IUserHealthStatusRepoistory userHealthStatusRepoistory , IMapper automapper)
            {
                this.userHealthStatusRepoistory = userHealthStatusRepoistory;
                this.automapper = automapper;
            }

     

            // Get All User Health Statuses
            public async Task<List<UserHealthStatusDTO>> GetAllUserHealthStatuses(string id)
            {
                var healthStatuses = await userHealthStatusRepoistory.GetAllUserHealthStatus( id);
                return automapper.Map<List<UserHealthStatusDTO>>(healthStatuses);
            }

            // Get User Health Status by ID
            public async Task<UserHealthStatusDTO> GetUserHealthStatusById(int id)
            {
                var healthStatus = await userHealthStatusRepoistory.GetUserHealthStatusByID(id);
                return automapper.Map<UserHealthStatusDTO>(healthStatus);
            }

            // Add New User Health Status
            public async Task<UserHealthStatusDTO> AddUserHealthStatus(UserHealthStatusDTO userHealthStatusDTO)
            {
                var userHealthStatus = automapper.Map<UserHealthStatus>(userHealthStatusDTO);
                var addedHealthStatus = await userHealthStatusRepoistory.AddUserHealthStatus(userHealthStatus);
                return automapper.Map<UserHealthStatusDTO>(addedHealthStatus);
            }

            // Delete User Health Status
            public async Task<int> DeleteUserHealthStatus(int id)
            {
                return await userHealthStatusRepoistory.DeleteUserHealthStatus(id);
            }

            // Calculate User Health Status Score
            public async Task<int> CalculateScore(int userHealthStatusID)
            {
                      var healthStatus = await userHealthStatusRepoistory.GetUserHealthStatusByID(userHealthStatusID);
                      
                      if (healthStatus == null) return -1;

                      return await userHealthStatusRepoistory.CalculateScore(healthStatus);
            }
         
        }
    }