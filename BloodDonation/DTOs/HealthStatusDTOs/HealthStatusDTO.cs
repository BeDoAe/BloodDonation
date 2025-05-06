using System.ComponentModel.DataAnnotations;

namespace BloodDonation.DTOs.HealthStatusDTOs
{
    public class HealthStatusDTO
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Range(0, 100)]
        public int LeastDegree { get; set; }

        [Required]
        public int Category { get; set; } // Enum: Donor = 0, Recipient = 1

        [MinLength(1, ErrorMessage = "At least one question is required.")]
        public List<HealthQuestionDTO> Questions { get; set; } = new List<HealthQuestionDTO>();
    }
}
