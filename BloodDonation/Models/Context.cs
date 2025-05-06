using BloodDonation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Emit;
using System.Reflection;

namespace BloodDonation.Models
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Reciepent> Reciepents { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<HealthStatus> HealthStatuses { get; set; }
        public DbSet<HealthQuestion> HealthQuestions { get; set; }
        public DbSet<UserHealthStatus> UserHealthStatuses { get; set; }
        public DbSet<UserHealthAnswer> UserHealthAnswers { get; set; }
        public DbSet<RequestBlood> RequestBloods { get; set; }
        public DbSet<DonateBlood> DonateBloods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPT Mapping (Table Per Type)
            modelBuilder.Entity<Reciepent>().ToTable("Reciepents");
            modelBuilder.Entity<Donor>().ToTable("Donors");
            modelBuilder.Entity<Hospital>().ToTable("Hospitals");

            // Apply global query filters for entities inheriting BaseModel
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(Context)
                        .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)
                        ?.MakeGenericMethod(entityType.ClrType);
                    method?.Invoke(null, new object[] { modelBuilder });
                }
            }

            // Apply query filters for ApplicationUser and its derived classes
            modelBuilder.Entity<ApplicationUser>()
                .HasQueryFilter(x => !x.IsDeleted && !x.IsLocked);

            // Define relationships

            // UserHealthStatus now correctly links to ApplicationUser
            modelBuilder.Entity<UserHealthStatus>()
                .HasOne(u => u.User)
                .WithMany(a => a.UserHealthStatuses)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // HealthStatus Relationship
            modelBuilder.Entity<UserHealthStatus>()
                .HasOne(u => u.HealthStatus)
                .WithMany()
                .HasForeignKey(u => u.HealthStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HealthQuestion>()
               .Property(hq => hq.Id)
               .ValueGeneratedOnAdd(); // Makes it auto-increment


            //modelBuilder.Entity<UserHealthStatus>()
            //    .HasOne(u => u.Reciepent)
            //    .WithMany(r => r.UserHealthStatuses)
            //    .HasForeignKey(u => u.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<UserHealthStatus>()
            //    .HasOne(u => u.Donor)
            //    .WithMany(d => d.UserHealthStatuses)
            //    .HasForeignKey(u => u.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RequestBlood>()
                .HasOne(r => r.Reciepent)
                .WithMany(r => r.requestBloods)
                .HasForeignKey(r => r.ReciepentID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RequestBlood>()
                .HasOne(r => r.Hospital)
                .WithMany(h => h.requestBloods)
                .HasForeignKey(r => r.HospitalID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DonateBlood>()
                .HasOne(d => d.Donor)
                .WithMany(d => d.donateBloods)
                .HasForeignKey(d => d.DonorID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DonateBlood>()
                .HasOne(d => d.Hospital)
                .WithMany(h => h.donateBloods)
                .HasForeignKey(d => d.HospitalID)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void SetGlobalQueryFilter<T>(ModelBuilder modelBuilder) where T : BaseModel
        {
            modelBuilder.Entity<T>().HasQueryFilter(m => !m.IsDeleted && !m.IsLocked);
        }
    }
}