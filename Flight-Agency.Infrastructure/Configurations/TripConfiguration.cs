using FlightAgency.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasMany(t => t.Stops)
                .WithOne();
        builder.Property(t => t.Destination)
                .IsRequired();
    }
}