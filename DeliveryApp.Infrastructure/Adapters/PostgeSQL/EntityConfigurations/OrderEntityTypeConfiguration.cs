using DeliveryApp.Core.Domain.Model.OrderAggegate;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryApp.Infrastructure.Adapters.PostgeSQL.EntityConfigurations;

internal class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entityTypeBuilder)
    {
        entityTypeBuilder.ToTable("Orders");
        entityTypeBuilder.HasKey(entity => entity.Id);

        // Id
        entityTypeBuilder
            .Property(entity => entity.Id)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .IsRequired();

        // Status
        entityTypeBuilder
            .OwnsOne(entity => entity.Status, a => { a.Property(c => c.Name).HasColumnName("status").IsRequired(); });
        entityTypeBuilder.Navigation(entity => entity.Status).IsRequired();

        // Location
        entityTypeBuilder
            .OwnsOne(entity => entity.Location, a =>
            {
                a.Property(c => c.X).HasColumnName("location_x").IsRequired(true);
                a.Property(c => c.Y).HasColumnName("location_y").IsRequired(true);
            });
        entityTypeBuilder.Navigation(e => e.Location).IsRequired();

        // Volume
        entityTypeBuilder
            .OwnsOne(e => e.Volume, p => { p.Property(v => v.Capatity).HasColumnName("volume").IsRequired(); });
        entityTypeBuilder.Navigation(e => e.Volume).IsRequired();
    }
}
