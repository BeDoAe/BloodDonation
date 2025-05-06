using BloodDonation.Models.Enums;

namespace BloodDonation.Models
{
    public class Donor : ApplicationUser
    {

        public DateTime? LastDonationDateTime { get; set; }

        public int Age { get; set; }
        public Gender gender { get; set; }
     
        public TypeOfBlood Blood { get; set; }

      


        public List<DonateBlood>? donateBloods { get; set; }

        public DateTime? LastHealthCheck { get; set; }
    }
}
