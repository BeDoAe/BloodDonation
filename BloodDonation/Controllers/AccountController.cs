using BloodDonation.DTOs.AccountDTOs;
using BloodDonation.Models;
using BloodDonation.Services.AccountServ;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonation.Controllers
{
    public class AccountController : Controller
    {


        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService accountService , UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public IActionResult Home()
        {
            return View();
        }


        [HttpPost("register-recipient")]
        public async Task<IActionResult> RegisterRecipient([FromBody] RegisterRecipientDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterRecipientAsync(model);
                if (!result.Success) return BadRequest(result.Message);
                return Ok(result.Data);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPost("register-donor")]
        public async Task<IActionResult> RegisterDonor([FromBody] RegisterDonorDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterDonorAsync(model);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPost("register-hospital")]
        public async Task<IActionResult> RegisterHospital([FromBody] RegisterHospitalDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterHospitalAsync(model);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login request.");

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid username or password.");

            var roles = await userManager.GetRolesAsync(user);
            string userRole = roles.FirstOrDefault() ?? "Unknown"; // Get first role or default to "Unknown"

            // **1️⃣ Create Claims**
            List<Claim> claims = new List<Claim>
                  {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, userRole)
                   };

            // **2️⃣ Create Identity & Principal**
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // **3️⃣ Sign In the User with Claims**
            await signInManager.SignInAsync(user, model.RememberMe);

            // **4️⃣ Return User Data**
            var response = new LoginResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Role = userRole
            };

            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "User logged out successfully." });
        }

    }
}