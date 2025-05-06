using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;

namespace BloodDonation.Repositories.DonateBloodRepo
{
    public interface IDonateBloodRepository : IRepository<DonateBlood>
    {
        public Task<Donor> GetDonorByID(string id);
        public Task<List<DonateBlood>> getAllBlooodDonationsOfUser(string id);
        public Task<List<DonateBlood>> getAllBlooodDonationsOfHospital(string id);
        public Task<DonateBlood> Donate(DonateBlood donateBlood);

        public  Task<DonateBlood> EditDonate(int id , DonateBlood donateBlood);

        public Task<int> DeleteDonate(int id);

    }
}