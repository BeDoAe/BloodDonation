using AutoMapper;
using BloodDonation.DTOs.DonatBloodDTOs;
using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.DTOs.UserHealthStatusDTOs;
using BloodDonation.Models;
using BloodDonation.Repositories.DonateBloodRepo;
using BloodDonation.Services.GenericServ;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BloodDonation.Services.DonateBloodServ
{
    public class DonateBloodService:Service<DonateBlood>, IDonateBloodService
    {
        private readonly IDonateBloodRepository donateBloodRepository;
        private readonly IMapper automapper;

       public DonateBloodService(IDonateBloodRepository donateBloodRepository , IMapper automapper)
        {
            this.donateBloodRepository = donateBloodRepository;
            this.automapper = automapper;
        }
        public async Task<DonorDTO> GetDonorByID(string id)
        {
            Donor donor = await donateBloodRepository.GetDonorByID(id);
            if (donor == null) return null;


            return automapper.Map<DonorDTO>(donor);

        }
        public async Task<List<DonateBloodDTO>> GetAllBlooodDonationsOfUser(string id)
        {
            var donateBloods = await donateBloodRepository.getAllBlooodDonationsOfUser(id);
            return automapper.Map<List<DonateBloodDTO>>(donateBloods);
        }

        public async Task<List<DonateBloodDTO>> GetAllBlooodDonationsOfHospital(string id)
        {
            var donateBloods = await donateBloodRepository.getAllBlooodDonationsOfHospital(id);
            return automapper.Map<List<DonateBloodDTO>>(donateBloods);
        }

        public async Task<DonateBloodDTO> Donate(DonateBloodDTO donateDto)
        {
            var donateBlood = automapper.Map<DonateBlood>(donateDto);
            var result = await donateBloodRepository.Donate(donateBlood);
            return result == null ? null : automapper.Map<DonateBloodDTO>(result);
        }

        public async Task<DonateBloodDTO> EditDonate(int id , DonateBloodDTO donateDto)
        {
            var donateBlood = automapper.Map<DonateBlood>(donateDto);
            var result = await donateBloodRepository.EditDonate(id ,donateBlood);
            return result == null ? null : automapper.Map<DonateBloodDTO>(result);
        }

        public async Task<int> DeleteDonate(int id)
        {
            return await donateBloodRepository.DeleteDonate(id);
        }
    }
}
