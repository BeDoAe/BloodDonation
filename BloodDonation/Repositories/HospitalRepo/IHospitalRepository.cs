using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;

namespace BloodDonation.Repositories.HospitalRepo
{
    public interface IHospitalRepository : IRepository<Hospital>
    {
        public  Task<List<Donor>> getAllDonorsOfHospital(string id);
        public Task<List<Reciepent>> getAllReciepentsOfHospital(string id);
        public Task<List<Hospital>> getAllHospitals();
        public Task<Hospital> GetHospitalByID(string id);
        public Task<List<DonateBlood>> getAllBlooodDonationsOfHospital(string id);
        public Task<int> getAllAvailableBloodAmount(string id);

    }
}