using BloodDonation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Repositories.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountRepository(UserManager<ApplicationUser> userManager )
        {
            this.userManager = userManager;
        }

        public async Task<bool> UserExists(string username, string email)
        {
            bool FoundUser = await userManager.Users.AnyAsync(u => u.UserName == username || u.Email == email);

            if (FoundUser == true)
            {
                return true;
            }
            else return false;

            

        }
    }
}