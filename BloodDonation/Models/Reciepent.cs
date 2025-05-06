using BloodDonation.Models.Enums;

namespace BloodDonation.Models
{
    public class Reciepent : ApplicationUser
    {


        public DateTime? LastRecieveDateTime { get; set; }

        public int Age { get; set; }
        public Gender gender { get; set; }

        public TypeOfBlood Blood { get; set; }


        public List<RequestBlood>? requestBloods { get; set; }
        public DateTime? LastHealthCheck { get; set; }
    }
}