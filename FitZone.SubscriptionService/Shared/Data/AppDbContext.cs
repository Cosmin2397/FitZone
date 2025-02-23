using FitZone.SubscriptionService.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FitZone.SubscriptionService.Shared.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
 : DbContext(options)
    {
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<PersonalTrainerSubscription> PersonalTrainerSubscription => Set<PersonalTrainerSubscription>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
