namespace BloodDonation.Models
{
    public class Hospital : ApplicationUser 
    {

 
        public List<DonateBlood>? donateBloods { get; set; }

        public List<RequestBlood>? requestBloods { get; set; }

        public int TotalBloodAmount { get; set; } = 0;

    }
}
