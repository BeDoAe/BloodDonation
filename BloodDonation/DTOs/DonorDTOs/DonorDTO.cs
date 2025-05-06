using BloodDonation.Models.Enums;

namespace BloodDonation.DTOs.DonorDTOs
{
    public class DonorDTO
    {
        public string? id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }

        public string Address { get; set; }

        public string? Image { get; set; }
        public DateTime? LastHealthCheck { get; set; }

        public DateTime? LastDonationDateTime { get; set; }

        public int Age { get; set; }
        public Gender gender { get; set; }

        public TypeOfBlood Blood { get; set; }

    }
}
