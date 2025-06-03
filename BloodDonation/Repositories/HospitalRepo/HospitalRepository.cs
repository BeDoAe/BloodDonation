using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BloodDonation.Repositories.HospitalRepo
{
    public class HospitalRepository : Repository<Hospital> , IHospitalRepository
    {
        private readonly Context context;

        public HospitalRepository(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<Donor>> getAllDonorsOfHospital(string id)
        {
            List<Donor> donors = await context.Donors.Where(h=>h.Id==id).ToListAsync();
            return donors;
        }
        public async Task<List<Reciepent>> getAllReciepentsOfHospital(string id)
        {
            List<Reciepent> Reciepents = await context.Reciepents.Where(h => h.Id == id).ToListAsync();
            return Reciepents;
        }

        public async Task<List<Hospital>> getAllHospitals()
        {
            List<Hospital> hospitals = await context.Hospitals.ToListAsync();
            return hospitals;
        }
        public async Task<Hospital> GetHospitalByID(string id)
        {
            Hospital hospital = await context.Hospitals
                .Include(h=>h.donateBloods)
                .Include(h=>h.requestBloods)
                .FirstOrDefaultAsync(d => d.Id == id);
            return hospital;
        }
        public async Task<List<DonateBlood>> getAllBlooodDonationsOfHospital(string id )
        {
            List<DonateBlood> BlooodDonations = await context.DonateBloods
                .Include(d => d.Hospital)
                .Include(d => d.Donor)
                .Where(h => h.HospitalID == id)
                .ToListAsync();
                
            return BlooodDonations;
        }
        public async Task<int> getAllAvailableBloodAmount(string id)
        {
            int? AvailableBloodAmount = await context.Hospitals
                .Where(h => h.Id == id)
                .Select(h => h.TotalBloodAmount)
                .FirstOrDefaultAsync();
            return AvailableBloodAmount??0;
        }
    }
}
