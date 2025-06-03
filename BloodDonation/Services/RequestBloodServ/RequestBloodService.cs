using AutoMapper;
using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.DTOs.ReciepentDTOs;
using BloodDonation.DTOs.RequestBloodDTOs;
using BloodDonation.Models;
using BloodDonation.Repositories.DonateBloodRepo;
using BloodDonation.Repositories.RequestBloodRepo;
using BloodDonation.Services.GenericServ;

namespace BloodDonation.Services.RequestBloodServ
{
    public class RequestBloodService : Service<RequestBlood> , IRequestBloodService
    {
        private readonly Context context;
        private readonly IRequestBloodRepository requestBloodRepository;
        private readonly IMapper automapper;

        public RequestBloodService(IRequestBloodRepository requestBloodRepository , IMapper automapper)
        {
            this.requestBloodRepository = requestBloodRepository;
            this.automapper = automapper;
        }
        public async Task<RecpientDTO> GetRecpientByID(string id)
        {
            Reciepent reciepent = await requestBloodRepository.GetReciepentByID(id);
            if (reciepent == null) return null;


            return automapper.Map<RecpientDTO>(reciepent);

        }
        public async Task<List<RequestBloodDTO>> GetAllRequestsByUser(string userId)
        {
            var requestList = await requestBloodRepository.getAllRequestBloodOfUser(userId);
            return automapper.Map<List<RequestBloodDTO>>(requestList);
        }

        public async Task<List<RequestBloodDTO>> GetAllRequestsByHospital(string hospitalId)
        {
            var requestList = await requestBloodRepository.getAllRequestBloodOfHospital(hospitalId);
            return automapper.Map<List<RequestBloodDTO>>(requestList);
        }

        public async Task<RequestBloodDTO> Request(RequestBloodDTO dto)
        {
            var requestEntity = automapper.Map<RequestBlood>(dto);
            var result = await requestBloodRepository.Request(requestEntity);

            return result != null ? automapper.Map<RequestBloodDTO>(result) : null;
        }

        public async Task<RequestBloodDTO> EditRequest(RequestBloodDTO dto)
        {
            var requestEntity = automapper.Map<RequestBlood>(dto);
            var result = await requestBloodRepository.EditRequest(requestEntity);

            return result != null ? automapper.Map<RequestBloodDTO>(result) : null;
        }

        public async Task<int> DeleteRequest(int id)
        {
            return await requestBloodRepository.DeleteRequest(id);
        }
    }
}