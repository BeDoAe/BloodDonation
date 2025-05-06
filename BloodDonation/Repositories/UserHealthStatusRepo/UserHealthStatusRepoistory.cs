using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.Repositories.UserHealthStatusRepo
{
    public class UserHealthStatusRepoistory : Repository<UserHealthStatus> , IUserHealthStatusRepoistory
    {
        private readonly Context context;

        public UserHealthStatusRepoistory(Context context) : base(context)
        {
            this.context = context;
        }




        public async Task<List<UserHealthStatus>> GetAllUserHealthStatus(string id)
        {
            List<UserHealthStatus> UsershealthStatuses = await context.UserHealthStatuses.Include(x => x.Answers).Where(u=>u.UserId==id
            ).ToListAsync();
            return UsershealthStatuses;
        }
        public async Task<UserHealthStatus> GetUserHealthStatusByID(int id)
        {
            UserHealthStatus UserhealthStatuses = await context.UserHealthStatuses.Include(x => x.Answers).FirstOrDefaultAsync(x => x.Id == id);
            return UserhealthStatuses;
        }
        //public async Task<HealthStatus> takeTest(int testID , string userID)
        //{

        //}
        public async Task<UserHealthStatus> AddUserHealthStatus(UserHealthStatus UserHealthStatus)
        {
            if (UserHealthStatus.Answers.IsNullOrEmpty()) return null;
            
            var entry = await context.UserHealthStatuses.AddAsync(UserHealthStatus);
            await SaveDB();
            var savedUserHealthStatus = entry.Entity; // This has the Id

            //var healthQuestions = await AddHealthQuestions(savedHealthStatus.Id, savedHealthStatus.Questions);
            //if (healthQuestions == null) return null;

            return savedUserHealthStatus;

        }
        public async Task<int> CalculateScore(UserHealthStatus userHealthStatus)
        {
            UserHealthStatus userHealthStatusFromDB = await context.UserHealthStatuses.FirstOrDefaultAsync(u => u.Id == userHealthStatus.Id);
            HealthStatus healthStatus = await context.HealthStatuses.FirstOrDefaultAsync(h => h.Id == userHealthStatus.HealthStatusId);

            if (userHealthStatusFromDB == null || healthStatus == null) return -1;

            int score = 100;
            foreach(var Answer in userHealthStatus.Answers)
            {
                if (score > 0)
                {
                    if (Answer.Answer == true)
                    score -= 10;
                }
                    
            }
            userHealthStatusFromDB.Score = score;
            await SaveDB();
            return score;

        }

        public async Task<int> DeleteUserHealthStatus(int UserhealthStatusId)
        {
            var UserhealthStatus = await context.UserHealthStatuses.Include(h => h.Answers).FirstOrDefaultAsync(h => h.Id == UserhealthStatusId);
            if (UserhealthStatus == null) return -1; // Not found

            if (UserhealthStatus.IsDeleted) return 0; // Already deleted

            UserhealthStatus.IsDeleted = true;
            foreach (var answer in UserhealthStatus.Answers)
            {
                answer.IsDeleted = true;
            }

            await SaveDB();
            return 1; // Deleted successfully
        }
    }
}
