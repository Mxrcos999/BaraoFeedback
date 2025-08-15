using BaraoFeedback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaraoFeedback.Infra.Mappings;

public class InstitutionMap : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable("Institution");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(i => i.Cep)
               .IsRequired()
               .HasMaxLength(10);
    }
}
