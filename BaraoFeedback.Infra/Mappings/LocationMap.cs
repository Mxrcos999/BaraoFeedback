using BaraoFeedback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaraoFeedback.Infra.Mappings;

public class LocationMap : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Location");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(l => l.Description)
               .IsRequired();

        builder.HasOne(l => l.Institution)
               .WithMany()
               .HasForeignKey(l => l.InstitutionId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
