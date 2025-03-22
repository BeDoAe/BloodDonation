using Microsoft.AspNetCore.Identity;

namespace BloodDonation.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }

        public string Address { get; set; }

        public string?  Image { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsLocked { get; set; } = false;
        public List<UserHealthStatus>? UserHealthStatuses { get; set; }


    }
}