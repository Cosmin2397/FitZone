using FitZone.SubscriptionService.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitZone.SubscriptionService.Shared.Data.Configurations
{
    public class PaymentConfiguration: IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(c => c.Id).IsRequired();
            builder.Property(c => c.SubscriptionId).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.Amount).IsRequired();
            builder.Property(c => c.PaymentDate).IsRequired();

    }
}
}
