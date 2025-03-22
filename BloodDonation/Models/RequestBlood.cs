namespace BloodDonation.Models
{
    public class RequestBlood : BaseModel
    {
        public int Id { get; set; }

        public string ReciepentID { get; set; }
        public Reciepent? Reciepent { get; set; }

        public string HospitalID { get; set; }
        public Hospital? Hospital { get; set; }

        public int Amount { get; set; }

        public DateTime RequestTime { get; set; } = DateTime.Now;

        //public bool? Approved { get; set; }
    }
}
