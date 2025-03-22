using AutoMapper;
using System.Text.RegularExpressions;
using BloodDonation.DTOs.AccountDTOs;
using BloodDonation.DTOs;
using BloodDonation.Models;
using BloodDonation.Repositories.AccountRepo;
using BloodDonation.Services.GenericServ;
using Microsoft.AspNetCore.Identity;

namespace BloodDonation.Services.AccountServ
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IAccountRepository accountRepository,
                              IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<RegisterRecipientDTO>> RegisterRecipientAsync(RegisterRecipientDTO model)
        {
            if (await _accountRepository.UserExists(model.UserName, model.Email))
                return new ServiceResponse<RegisterRecipientDTO> { Success = false, Message = "Username or Email already exists." };

            var recipient = _mapper.Map<Reciepent>(model);
            var result = await _userManager.CreateAsync(recipient, model.Password);

            if (!result.Succeeded)
                return new ServiceResponse<RegisterRecipientDTO> { Success = false, Message = "Failed to register recipient." };

            await AssignRoleAsync(recipient, "Recipient");

            return new ServiceResponse<RegisterRecipientDTO> { Success = true, Data = model };
        }

        public async Task<ServiceResponse<RegisterDonorDTO>> RegisterDonorAsync(RegisterDonorDTO model)
        {
            if (await _accountRepository.UserExists(model.UserName, model.Email))
                return new ServiceResponse<RegisterDonorDTO> { Success = false, Message = "Username or Email already exists." };

            var donor = _mapper.Map<Donor>(model);
            var result = await _userManager.CreateAsync(donor, model.Password);

            if (!result.Succeeded)
                return new ServiceResponse<RegisterDonorDTO> { Success = false, Message = "Failed to register donor." };

            await AssignRoleAsync(donor, "Donor");

            return new ServiceResponse<RegisterDonorDTO> { Success = true, Data = model };
        }

        public async Task<ServiceResponse<RegisterHospitalDTO>> RegisterHospitalAsync(RegisterHospitalDTO model)
        {
            if (await _accountRepository.UserExists(model.UserName, model.Email))
                return new ServiceResponse<RegisterHospitalDTO> { Success = false, Message = "Hospital UserName  or Email already exists." };

            var hospital = _mapper.Map<Hospital>(model);

            // Clean the username by removing spaces & special characters
            //hospital.UserName = Regex.Replace(model.UserName, @"[^a-zA-Z0-9]", "");

            var result = await _userManager.CreateAsync(hospital, model.Password);

            if (!result.Succeeded)
                return new ServiceResponse<RegisterHospitalDTO> { Success = false, Message = "Failed to register hospital." };

            await AssignRoleAsync(hospital, "Hospital");

            return new ServiceResponse<RegisterHospitalDTO> { Success = true, Data = model };
        }

        private async Task AssignRoleAsync(ApplicationUser user, string roleName)
        {
            // Check if the role exists; if not, create it
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Assign role to the user
            await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}