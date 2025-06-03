using AutoMapper;
using BloodDonation.DTOs.DonatBloodDTOs;
using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.DTOs.Hospital;
using BloodDonation.DTOs.ReciepentDTOs;
using BloodDonation.Repositories.HospitalRepo;

namespace BloodDonation.Services.HospitalServ
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository hospitalRepository;
        private readonly IMapper automapper;

        public HospitalService(IHospitalRepository hospitalRepository , IMapper automapper)
        {
            this.hospitalRepository = hospitalRepository;
            this.automapper = automapper;
        }
        public async Task<List<HospitalDTO>> GetAllHospitals()
        {
            var hospitals = await hospitalRepository.getAllHospitals();
            return automapper.Map<List<HospitalDTO>>(hospitals);
        }

        public async Task<HospitalDTO> GetHospitalById(string id)
        {
            var hospital = await hospitalRepository.GetHospitalByID(id);
            return automapper.Map<HospitalDTO>(hospital);
        }

        public async Task<List<DonorDTO>> GetAllDonorsOfHospital(string id)
        {
            var donors = await hospitalRepository.getAllDonorsOfHospital(id);
            return automapper.Map<List<DonorDTO>>(donors);
        }

        public async Task<List<RecpientDTO>> GetAllReciepentsOfHospital(string id)
        {
            var reciepents = await hospitalRepository.getAllReciepentsOfHospital(id);
            return automapper.Map<List<RecpientDTO>>(reciepents);
        }

        public async Task<List<DonateBloodDTO>> GetAllBloodDonationsOfHospital(string id)
        {
            var donations = await hospitalRepository.getAllBlooodDonationsOfHospital(id);
            return automapper.Map<List<DonateBloodDTO>>(donations);
        }

        public async Task<int> GetAvailableBloodAmount(string id)
        {
            return await hospitalRepository.getAllAvailableBloodAmount(id);
        }
    }
}