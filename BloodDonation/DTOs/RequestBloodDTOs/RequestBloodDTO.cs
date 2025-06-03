using BloodDonation.Models;
using BloodDonation.DTOs.Hospital;
using BloodDonation.DTOs.ReciepentDTOs;

namespace BloodDonation.DTOs.RequestBloodDTOs
{
    public class RequestBloodDTO
    {
        public int Id { get; set; }

        public string ReciepentID { get; set; }
        public RecpientDTO? Reciepent { get; set; }

        public string HospitalID { get; set; }
        public HospitalDTO? Hospital { get; set; }

        public int Amount { get; set; }

        public DateTime RequestTime { get; set; } = DateTime.Now;
    }
}
