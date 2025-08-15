using BaraoFeedback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaraoFeedback.Infra.Mappings;

public class TicketCategoryMap : IEntityTypeConfiguration<TicketCategory>
{
    public void Configure(EntityTypeBuilder<TicketCategory> builder)
    {
        builder.ToTable("TicketCategory");

        builder.HasKey(tc => tc.Id);

        builder.Property(tc => tc.Description)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(tc => tc.IsActive)
               .IsRequired();
    }
}
