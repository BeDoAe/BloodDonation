using BloodDonation.DTOs.DonatBloodDTOs;
using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.Models;
using BloodDonation.Services.GenericServ;

namespace BloodDonation.Services.DonateBloodServ
{
    public interface IDonateBloodService : IService<DonateBlood>
    {
        public Task<DonorDTO> GetDonorByID(string id);
        public Task<List<DonateBloodDTO>> GetAllBlooodDonationsOfUser(string id);
        public Task<List<DonateBloodDTO>> GetAllBlooodDonationsOfHospital(string id);
        public Task<DonateBloodDTO> Donate(DonateBloodDTO donateDto);
        public Task<DonateBloodDTO> EditDonate(int id , DonateBloodDTO donateDto);
        public Task<int> DeleteDonate(int id);

    }
}