using BaraoFeedback.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaraoFeedback.Infra.Mappings;

public class TicketMap : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Ticket");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(t => t.Description)
               .IsRequired();

        builder.Property(t => t.Processed)
               .IsRequired();

        // Configuração das foreign keys com NoAction para evitar problemas de cascade paths
        builder.HasOne(t => t.ApplicationUser)
               .WithMany()
               .HasForeignKey(t => t.ApplicationUserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.TicketCategory)
               .WithMany()
               .HasForeignKey(t => t.TicketCategoryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.Institution)
               .WithMany()
               .HasForeignKey(t => t.InstitutionId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.Location)
               .WithMany()
               .HasForeignKey(t => t.LocationId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
