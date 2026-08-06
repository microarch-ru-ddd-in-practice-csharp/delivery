using DeliveryApp.Core.Domain.Model.CounterAggegate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Adapters.PostgeSQL.EntityConfigurations;

internal class CourierEntityConfiguration : IEntityTypeConfiguration<Courier>
{
    public void Configure(EntityTypeBuilder<Courier> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Couriers");
        entityTypeBuilder.HasKey(entity => entity.Id);

        // Id
        entityTypeBuilder
            .Property(entity => entity.Id)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .IsRequired();

        // Location
        entityTypeBuilder
            .OwnsOne(entity => entity.Location, a =>
            {
                a.Property(c => c.X).HasColumnName("location_x").IsRequired(true);
                a.Property(c => c.Y).HasColumnName("location_y").IsRequired(true);
            });
        entityTypeBuilder.Navigation(e => e.Location).IsRequired();

        // MaxVolume
        entityTypeBuilder
            .OwnsOne(e => e.MaxVolume, p => { p.Property(v => v.Capatity).HasColumnName("max_volume").IsRequired(); });
        entityTypeBuilder.Navigation(e => e.MaxVolume).IsRequired();

        // Title
        entityTypeBuilder
            .Property(entity => entity.Name)
            .HasColumnName("name")
            .IsRequired();

        // Assignments
        entityTypeBuilder.HasMany(x => x.Assignments)
               .WithOne(x => x.Courier)
               .HasForeignKey(x => x.CourierId);

    }
}
