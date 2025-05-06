using BloodDonation.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonation.DTOs.UserHealthStatusDTOs
{
    public class UserHealthStatusDTO
    {
        public int Id { get; set; }

        public string? UserId { get; set; }


        public int HealthStatusId { get; set; }

        public List<UserHealthAnswerDTO> Answers { get; set; } = new List<UserHealthAnswerDTO>(); // One-to-Many

        public int? Score { get; set; }
        public DateTime UserHealthDate { get; set; } = DateTime.Now;
    }
}
