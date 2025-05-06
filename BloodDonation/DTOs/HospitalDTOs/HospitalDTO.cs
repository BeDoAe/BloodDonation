using BloodDonation.Models;

namespace BloodDonation.DTOs.Hospital
{
    public class HospitalDTO
    {
        public string? id { get; set; }

        public int? TotalBloodAmount { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }

        public string Address { get; set; }

        public string? Image { get; set; }
    }
}
