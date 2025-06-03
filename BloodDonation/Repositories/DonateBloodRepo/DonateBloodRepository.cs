using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Repositories.DonateBloodRepo
{
    public class DonateBloodRepository : Repository<DonateBlood>, IDonateBloodRepository
    {
        private readonly Context context;

        public DonateBloodRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Donor> GetDonorByID(string id)
        {
            Donor donor = await context.Donors
                .Include(h=>h.UserHealthStatuses)
                .Include(h=>h.donateBloods)
                
                .FirstOrDefaultAsync(d => d.Id == id);
            return donor;
        }


        public async Task<List<DonateBlood>> getAllBlooodDonationsOfUser(string id)
        {
            List<DonateBlood> BlooodDonations = await context.DonateBloods.Include(d => d.Hospital)
                .Include(d => d.Donor)
                .Where(u => u.DonorID == id)
                .ToListAsync();
            return BlooodDonations;
        }

        public async Task<List<DonateBlood>> getAllBlooodDonationsOfHospital(string id)
        {
            List<DonateBlood> BlooodDonations = await context.DonateBloods.Include(d => d.Hospital)
                .Include(d => d.Donor)
                .Where(h => h.HospitalID == id)
                .ToListAsync();
            return BlooodDonations;
        }

      




        public async Task<DonateBlood> Donate(DonateBlood donateBlood)
        {
            Donor donor = await context.Donors.FirstOrDefaultAsync(d => d.Id == donateBlood.DonorID);
            Hospital hospital = await context.Hospitals.FirstOrDefaultAsync(h => h.Id == donateBlood.HospitalID);

            if (donor == null || hospital == null || donateBlood.Amount <0) return null;

            await context.AddAsync(donateBlood);
            hospital.TotalBloodAmount += donateBlood.Amount;
            donor.LastDonationDateTime = DateTime.Now;
            context.Update(hospital);
            await SaveDB();
            return donateBlood;
        }
        public async Task<DonateBlood> EditDonate(int id , DonateBlood donateBlood)
        {
            Donor donor = await context.Donors.FirstOrDefaultAsync(d => d.Id == donateBlood.DonorID);
            Hospital hospital = await context.Hospitals.FirstOrDefaultAsync(h => h.Id == donateBlood.HospitalID);
            DonateBlood donateBloodFromDB = await context.DonateBloods.FirstOrDefaultAsync(db => db.Id == id);

            if (donor == null || hospital == null || donateBlood.Amount < 0 || donateBloodFromDB==null) return null;
            donateBloodFromDB.HospitalID = donateBlood.HospitalID;
            hospital.TotalBloodAmount -= donateBloodFromDB.Amount;

            donateBloodFromDB.Amount = donateBlood.Amount;
            donateBloodFromDB.DonationTime= DateTime.Now;

            hospital.TotalBloodAmount += donateBlood.Amount;
            context.Update(donateBloodFromDB);
            await SaveDB();
            return donateBloodFromDB;
        }
        public async Task<int> DeleteDonate(int id)
        {
            
            DonateBlood donateBloodFromDB = await context.DonateBloods.FirstOrDefaultAsync(db => db.Id == id);
            Hospital hospital = await context.Hospitals.FirstOrDefaultAsync(h => h.Id == donateBloodFromDB.HospitalID);

            if (donateBloodFromDB == null)
            {
                return -1; //Not Found
            }else if (donateBloodFromDB.IsDeleted==true)
            {
                return 0; // Already Deleted 
            }
            else
            {
                donateBloodFromDB.IsDeleted = true;
                hospital.TotalBloodAmount -= donateBloodFromDB.Amount;
                await SaveDB();

                return 1; // Delete successfully
            }
        
        }
    }
}
