using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;

namespace BloodDonation.Repositories.RequestBloodRepo
{
    public interface IRequestBloodRepository : IRepository<RequestBlood>
    {
        public Task<Reciepent> GetReciepentByID(string id);
        public Task<List<RequestBlood>> getAllRequestBloodOfUser(string id);
        public Task<List<RequestBlood>> getAllRequestBloodOfHospital(string id);
        public Task<RequestBlood> Request(RequestBlood requestBlood);
        public Task<RequestBlood> EditRequest(RequestBlood requestBlood);
        public Task<int> DeleteRequest(int id);

    }
}