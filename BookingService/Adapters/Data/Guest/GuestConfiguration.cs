using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Guests.Entities;



namespace Data.Guests
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasColumnType("text");
            builder.Property(e => e.Surname).HasColumnType("text");
            builder.Property(e => e.Email).HasColumnType("text");

            builder.OwnsOne(e => e.DocumentId, document =>
            {
                document.Property(d => d.IdNumber).HasColumnType("text").IsRequired();
                document.Property(d => d.DocumentType).HasColumnType("integer").IsRequired();
            });
        }

    }
}
