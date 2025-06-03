using AutoMapper;
using BloodDonation.DTOs.AccountDTOs;
using BloodDonation.DTOs.DonatBloodDTOs;
using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.DTOs.HealthStatusDTOs;
using BloodDonation.DTOs.Hospital;
using BloodDonation.DTOs.ReciepentDTOs;
using BloodDonation.DTOs.RequestBloodDTOs;
using BloodDonation.DTOs.UserHealthStatusDTOs;
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

            CreateMap<HealthStatus, HealthStatusDTO>()
                       .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
                       .ReverseMap();

            CreateMap<HealthQuestion, HealthQuestionDTO>().ReverseMap();


            CreateMap<UserHealthStatus, UserHealthStatusDTO>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers))
                .ReverseMap();

            

            CreateMap<UserHealthAnswer, UserHealthAnswerDTO>()
                 .ReverseMap();

            CreateMap<DonateBlood, DonateBloodDTO>().ReverseMap();

            CreateMap<Donor, DonorDTO>().ReverseMap();

            CreateMap<Hospital, HospitalDTO>().ReverseMap();


            CreateMap<RequestBlood, RequestBloodDTO>().ReverseMap();

            CreateMap<Reciepent, RecpientDTO>().ReverseMap();
        }
    }
}

