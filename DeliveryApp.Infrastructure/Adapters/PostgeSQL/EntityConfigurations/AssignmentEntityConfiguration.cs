using DeliveryApp.Core.Domain.Model.AssignmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Adapters.PostgeSQL.EntityConfigurations;

internal class AssignmentEntityConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("Assignments");
        builder.HasKey(entity => entity.Id);

        // Id
        builder
            .Property(entity => entity.Id)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .IsRequired();

        // Status
        builder
            .OwnsOne(entity => entity.Status, a => { a.Property(c => c.Name).HasColumnName("status").IsRequired(); });
        builder.Navigation(entity => entity.Status).IsRequired();

        // Location
        builder
            .OwnsOne(entity => entity.Location, a =>
            {
                a.Property(c => c.X).HasColumnName("location_x").IsRequired(true);
                a.Property(c => c.Y).HasColumnName("location_y").IsRequired(true);
            });
        builder.Navigation(e => e.Location).IsRequired();

        // Volume
        builder
            .OwnsOne(e => e.Volume, p => { p.Property(v => v.Capatity).HasColumnName("volume").IsRequired(); });
        builder.Navigation(e => e.Volume).IsRequired();

        // CourierId
        builder
            .Property(entity => entity.CourierId)
            .ValueGeneratedNever()
            .HasColumnName("courierid")
            .IsRequired();

        // Courier
        builder.HasOne(x => x.Courier)
               .WithMany(x => x.Assignments)
               .HasForeignKey(x => x.CourierId);
    }
}
