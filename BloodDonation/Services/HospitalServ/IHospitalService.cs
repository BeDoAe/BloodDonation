using BloodDonation.DTOs.DonatBloodDTOs;
using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.DTOs.Hospital;
using BloodDonation.DTOs.ReciepentDTOs;

namespace BloodDonation.Services.HospitalServ
{
    public interface IHospitalService
    {
        public Task<List<HospitalDTO>> GetAllHospitals();
        public Task<HospitalDTO> GetHospitalById(string id);
        public Task<List<DonorDTO>> GetAllDonorsOfHospital(string id);
        public Task<List<RecpientDTO>> GetAllReciepentsOfHospital(string id);
        public Task<List<DonateBloodDTO>> GetAllBloodDonationsOfHospital(string id);
        public Task<int> GetAvailableBloodAmount(string id);

    }
}