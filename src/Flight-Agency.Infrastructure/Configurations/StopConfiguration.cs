using FlightAgency.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightAgency.Infrastructure.Configurations;

public class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.HasKey(s => s.Id);

        builder.OwnsOne(s => s.Location, l =>
        {
            l.Property(l => l.Latitude).HasColumnName("Latitude");
            l.Property(l => l.Longitude).HasColumnName("Longitude");
        });

        builder.OwnsOne(s => s.Time, t =>
        {
            t.Property(t => t.Start).HasColumnName("Start");
            t.Property(t => t.End).HasColumnName("End");
        });

        builder.Property(s => s.Name).IsRequired();
        builder.Property(s => s.Address).IsRequired();
        builder.Property(s => s.Category).IsRequired();
    }
}