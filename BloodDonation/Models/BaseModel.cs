namespace BloodDonation.Models
{
    public class BaseModel
    {
        public bool IsLocked { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
