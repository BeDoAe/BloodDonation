using AutoMapper;
using BloodDonation.DTOs.AccountDTOs;
using BloodDonation.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BloodDonation.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRecipientDTO, Reciepent>().ReverseMap();
            CreateMap<RegisterDonorDTO, Donor>().ReverseMap();
            CreateMap<RegisterHospitalDTO, Hospital>().ReverseMap();
        }
    }
}

