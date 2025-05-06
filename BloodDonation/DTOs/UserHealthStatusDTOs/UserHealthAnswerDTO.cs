using BloodDonation.Models;

namespace BloodDonation.DTOs.UserHealthStatusDTOs
{
    public class UserHealthAnswerDTO
    {
        public int Id { get; set; }

        //public int UserHealthStatusId { get; set; }

        public int HealthQuestionId { get; set; }

        public bool Answer { get; set; }
    }
}
