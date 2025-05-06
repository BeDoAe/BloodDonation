using System.ComponentModel.DataAnnotations;

namespace BloodDonation.DTOs.HealthStatusDTOs
{
    public class HealthQuestionDTO
    {
        public int Id { get; set; }

        [Required, StringLength(500)]
        public string QuestionText { get; set; }

        //public int? HealthStatusId { get; set; }
    }
}
