using BloodDonation.DTOs.AccountDTOs;
using BloodDonation.Models;
using BloodDonation.Services.AccountServ;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BloodDonation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {


        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(IAccountService accountService , UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager , IConfiguration configuration)
        {
            _accountService = accountService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpGet]
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
            string role = roles.FirstOrDefault() ?? "Unknown";

            // 1️⃣ Add Claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, role)
    };

            // 2️⃣ Generate JWT Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // 3️⃣ Return token in response
            return Ok(new
            {
                token = jwtToken,
                expiration = token.ValidTo,
                id = user.Id,
                name = user.Name,
                userName = user.UserName,
                email = user.Email,
                address = user.Address,
                phoneNumber = user.PhoneNumber,
                role = role
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "User logged out successfully." });
        }

    }
}