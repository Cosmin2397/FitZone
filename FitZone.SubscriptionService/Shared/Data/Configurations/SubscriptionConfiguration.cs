using FitZone.SubscriptionService.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FitZone.SubscriptionService.Shared.Data.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.ClientId).IsRequired();
            builder.Property(c => c.GymId).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.ClientType).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.StartingDate).IsRequired();
            builder.Property(c => c.EndDate).IsRequired();
        }
    }
}
