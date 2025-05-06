using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;

namespace BloodDonation.Repositories.HospitalRepo
{
    public interface IHospitalRepository : IRepository<Hospital>
    {
        public Task<List<Donor>> getAllDonors(string id);
        public Task<List<Reciepent>> getAllReciepents(string id);
        public Task<List<Hospital>> getAllHospitals();
        public Task<Hospital> GetHospitalByID(string id);
        public Task<List<DonateBlood>> getAllBlooodDonations();
        public Task<int> getAllAvailableBloodAmount(string id);

    }
}