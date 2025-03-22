namespace BloodDonation.Models
{
    public class UserHealthAnswer : BaseModel
    {
        public int Id { get; set; }

        public int UserHealthStatusId { get; set; }
        public UserHealthStatus? UserHealthStatus { get; set; }  // Many-to-One

        public int HealthQuestionId { get; set; }
        public HealthQuestion? HealthQuestion { get; set; }  // Many-to-One

        public bool Answer { get; set; } // true = checked, false = not checked
    }
}