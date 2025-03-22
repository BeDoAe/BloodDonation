namespace BloodDonation.Models
{
    public class HealthStatus : BaseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public List<HealthQuestion> Questions { get; set; } = new List<HealthQuestion>(); // One-to-Many


        public int LeastDegree { get; set; }


        public Category category { get; set; }

        public enum Category
        {
            Donor = 0 ,
            Recipient = 1
        }
    }
}
