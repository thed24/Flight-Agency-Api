using FlightAgency.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.HasKey(s => s.Id);
        builder.OwnsOne(s => s.Time);
        builder.OwnsOne(s => s.Location);
        builder.Property(s => s.Name)
                .IsRequired();
        builder.Property(s => s.Address)
                .IsRequired();
        builder.Property(s => s.Category)
                .IsRequired();
    }
}