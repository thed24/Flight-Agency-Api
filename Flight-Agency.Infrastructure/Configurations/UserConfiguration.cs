using FlightAgency.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasMany(u => u.Trips)
                .WithOne();
        builder.Property(u => u.Name)
                .IsRequired();
        builder.Property(u => u.Email)
                .IsRequired();
        builder.Property(u => u.Password)
                .IsRequired();
    }
}