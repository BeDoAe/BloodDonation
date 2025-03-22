namespace BloodDonation.Models
{
    public class HealthQuestion : BaseModel
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public int HealthStatusId { get; set; }
        public HealthStatus? HealthStatus { get; set; }

    }
}