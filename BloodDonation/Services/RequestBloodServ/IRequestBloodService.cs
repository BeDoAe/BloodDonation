using BloodDonation.DTOs.ReciepentDTOs;
using BloodDonation.DTOs.RequestBloodDTOs;
using BloodDonation.Models;
using BloodDonation.Services.GenericServ;

namespace BloodDonation.Services.RequestBloodServ
{
    public interface IRequestBloodService : IService<RequestBlood>
    {
        public Task<RecpientDTO> GetRecpientByID(string id);
        public Task<List<RequestBloodDTO>> GetAllRequestsByUser(string userId);
        public Task<List<RequestBloodDTO>> GetAllRequestsByHospital(string hospitalId);
        public Task<RequestBloodDTO> Request(RequestBloodDTO dto);
        public Task<RequestBloodDTO> EditRequest(RequestBloodDTO dto);
        public Task<int> DeleteRequest(int id);


    }
}