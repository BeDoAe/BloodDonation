using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonation.Models
{
    public class UserHealthStatus : BaseModel
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; } //User In General


        public int HealthStatusId { get; set; }
        public HealthStatus? HealthStatus { get; set; }  // Many-to-One with HealthStatus

        public List<UserHealthAnswer> Answers { get; set; } = new List<UserHealthAnswer>(); // One-to-Many

        public int? Score { get; set; }
        public DateTime UserHealthDate { get; set; } = DateTime.Now;

    }
}
