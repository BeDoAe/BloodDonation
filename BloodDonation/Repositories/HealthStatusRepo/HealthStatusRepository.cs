using BloodDonation.Models;
using BloodDonation.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
namespace BloodDonation.Repositories.HealthStatusRepo
{
    public class HealthStatusRepository : Repository<HealthStatus>, IHealthStatusRepository
    {
        private readonly Context context;
        public HealthStatusRepository(Context context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<HealthStatus>> GetAllHealthStatus()
        {
            List<HealthStatus> healthStatuses = await context.HealthStatuses.Include(x=>x.Questions).ToListAsync();
            return healthStatuses;
        }
        public async Task<HealthStatus> GetHealthStatusByID(int id)
        {
            HealthStatus healthStatuses = await context.HealthStatuses.Include(x => x.Questions).FirstOrDefaultAsync(x=>x.Id==id);
            return healthStatuses;
        }
        public async Task<HealthStatus> AddHealthStatus(HealthStatus healthStatus)
        {
            if (healthStatus.Questions.IsNullOrEmpty()) return null;

            var entry = await context.HealthStatuses.AddAsync(healthStatus);
            await SaveDB();
            var savedHealthStatus = entry.Entity; // This has the Id

            //var healthQuestions = await AddHealthQuestions(savedHealthStatus.Id, savedHealthStatus.Questions);
            //if (healthQuestions == null) return null;

            return savedHealthStatus;

        }

        public async Task<List<HealthQuestion>> AddHealthQuestions(int healthStatusId, List<HealthQuestion> questions)
        {
            var healthStatus = await context.HealthStatuses.FindAsync(healthStatusId);
            if (healthStatus == null) return null;

            foreach (var question in questions)
            {
                question.HealthStatusId = healthStatusId;
                await context.HealthQuestions.AddAsync(question);
            }

            await context.SaveChangesAsync();
            return questions;
        }

        public async Task<HealthStatus> UpdateHealthStatus(HealthStatus updatedStatus)
        {
            var existingStatus = await context.HealthStatuses
                .Include(h => h.Questions)
                .FirstOrDefaultAsync(h => h.Id == updatedStatus.Id);

            if (existingStatus == null) return null;

            existingStatus.Title = updatedStatus.Title;
            existingStatus.LeastDegree = updatedStatus.LeastDegree;
            existingStatus.category = updatedStatus.category;

            var existingQuestions = existingStatus.Questions.ToList();

            foreach (var updatedQuestion in updatedStatus.Questions)
            {
                var existingQuestion = existingQuestions.FirstOrDefault(q => q.Id == updatedQuestion.Id);

                if (existingQuestion != null)
                {
                    existingQuestion.QuestionText = updatedQuestion.QuestionText;
                }
                else
                {
                    updatedQuestion.HealthStatusId = updatedStatus.Id;
                    existingStatus.Questions.Add(updatedQuestion);
                }
            }

           
            await SaveDB();
            return existingStatus;
        }


        public async Task<int> DeleteHealthStatus(int healthStatusId)
        {
            var healthStatus = await context.HealthStatuses.Include(h => h.Questions).FirstOrDefaultAsync(h => h.Id == healthStatusId);
            if (healthStatus == null) return -1; // Not found

            if (healthStatus.IsDeleted) return 0; // Already deleted

            healthStatus.IsDeleted = true;
            foreach (var question in healthStatus.Questions)
            {
                question.IsDeleted = true;
            }

            await SaveDB();
            return 1; // Deleted successfully
        }

        public async Task<int> DeleteHealthQuestion(int questionId)
        {
            var question = await context.HealthQuestions.FindAsync(questionId);
            if (question == null) return -1;

            if (question.IsDeleted) return 0;

            question.IsDeleted = true;
            await SaveDB();
            return 1;
        }
    }
}