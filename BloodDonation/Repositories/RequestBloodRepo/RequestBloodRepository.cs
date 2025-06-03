using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Repositories.RequestBloodRepo
{
    public class RequestBloodRepository : Repository<RequestBlood> , IRequestBloodRepository
    {
        private readonly Context context;

        public RequestBloodRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Reciepent> GetReciepentByID(string id)
        {
            Reciepent reciepent = await context.Reciepents
                .Include(h => h.UserHealthStatuses)
                .Include(h => h.requestBloods)

                .FirstOrDefaultAsync(d => d.Id == id);
            return reciepent;
        }


        public async Task<List<RequestBlood>> getAllRequestBloodOfUser(string id)
        {
            List<RequestBlood> requestBloods = await context.RequestBloods.Include(d => d.Hospital)
                .Include(d => d.Reciepent)
                .Where(u => u.ReciepentID == id)
                .ToListAsync();
            return requestBloods;
        }

        public async Task<List<RequestBlood>> getAllRequestBloodOfHospital(string id)
        {
            List<RequestBlood> requestBloods = await context.RequestBloods.Include(d => d.Hospital)
                .Include(d => d.Reciepent)
                .Where(h => h.HospitalID == id)
                .ToListAsync();
            return requestBloods;
        }






        public async Task<RequestBlood> Request(RequestBlood requestBlood)
        {
            Reciepent reciepent = await context.Reciepents.FirstOrDefaultAsync(d => d.Id == requestBlood.ReciepentID);
            Hospital hospital = await context.Hospitals.FirstOrDefaultAsync(h => h.Id == requestBlood.HospitalID);

            if (reciepent == null || hospital == null || requestBlood.Amount > 0) return null;

            await context.AddAsync(requestBlood);
            hospital.TotalBloodAmount -= requestBlood.Amount;
            reciepent.LastRecieveDateTime = DateTime.Now;
            await SaveDB();
            return requestBlood;
        }
        public async Task<RequestBlood> EditRequest(RequestBlood requestBlood)
        {
            Reciepent reciepent = await context.Reciepents.FirstOrDefaultAsync(d => d.Id == requestBlood.ReciepentID);
            Hospital hospital = await context.Hospitals.FirstOrDefaultAsync(h => h.Id == requestBlood.HospitalID);
            RequestBlood requestBloodFromDB = await context.RequestBloods.FirstOrDefaultAsync(db => db.Id == requestBlood.Id);

            if (reciepent == null || hospital == null || requestBlood.Amount > 0 || requestBloodFromDB == null) return null;
            requestBloodFromDB.HospitalID = requestBlood.HospitalID;
            hospital.TotalBloodAmount += requestBloodFromDB.Amount;

            requestBloodFromDB.Amount = requestBlood.Amount;
            requestBloodFromDB.RequestTime = DateTime.Now;

            hospital.TotalBloodAmount -= requestBlood.Amount;
            context.Update(requestBloodFromDB);
            await SaveDB();
            return requestBloodFromDB;
        }
        public async Task<int> DeleteRequest(int id)
        {

            RequestBlood requestBloodFromDB = await context.RequestBloods.FirstOrDefaultAsync(db => db.Id == id);
            Hospital hospital = await context.Hospitals.FirstOrDefaultAsync(h => h.Id == requestBloodFromDB.HospitalID);

            if (requestBloodFromDB == null)
            {
                return -1; //Not Found
            }
            else if (requestBloodFromDB.IsDeleted == true)
            {
                return 0; // Already Deleted 
            }
            else
            {
                requestBloodFromDB.IsDeleted = true;
                hospital.TotalBloodAmount += requestBloodFromDB.Amount;
                await SaveDB();

                return 1; // Delete successfully
            }

        }
    }
}
