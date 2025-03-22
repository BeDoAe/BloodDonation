using BloodDonation.DTOs.AccountDTOs;
using BloodDonation.DTOs;

namespace BloodDonation.Services.AccountServ
{
    public interface IAccountService
    {
        public Task<ServiceResponse<RegisterRecipientDTO>> RegisterRecipientAsync(RegisterRecipientDTO model);
        public Task<ServiceResponse<RegisterDonorDTO>> RegisterDonorAsync(RegisterDonorDTO model);
        public Task<ServiceResponse<RegisterHospitalDTO>> RegisterHospitalAsync(RegisterHospitalDTO model);
  
    }
}