namespace BloodDonation.Models
{
    public class DonateBlood : BaseModel
    {
        public int Id { get; set; }

        public string DonorID { get; set; }
        public Donor? Donor { get; set; }

        public string HospitalID { get; set; }
        public Hospital? Hospital { get; set; }

        public int Amount { get; set; }

        public DateTime DonationTime { get; set; } = DateTime.Now;

        //public bool? Approved { get; set; }
    }
}
