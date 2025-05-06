using BloodDonation.DTOs.DonorDTOs;
using BloodDonation.DTOs.Hospital;
using BloodDonation.Models;

namespace BloodDonation.DTOs.DonatBloodDTOs
{
    public class DonateBloodDTO
    {
        public int Id { get; set; }

        public string? DonorID { get; set; }
        public DonorDTO? Donor { get; set; }

        public string HospitalID { get; set; }
        public HospitalDTO? Hospital { get; set; }

        public int Amount { get; set; }

        public DateTime DonationTime { get; set; } = DateTime.Now;
    }
}
