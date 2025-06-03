using BloodDonation.Models.Enums;

namespace BloodDonation.DTOs.ReciepentDTOs
{
    public class RecpientDTO
    {
        public string? id { get; set; }

        public string? Name { get; set; }
        public string? LastName { get; set; }

        public string Address { get; set; }

        public string? Image { get; set; }
        public DateTime? LastRecieveDateTime { get; set; }
        public int Age { get; set; }
        public Gender gender { get; set; }

        public TypeOfBlood Blood { get; set; }
    }
}
