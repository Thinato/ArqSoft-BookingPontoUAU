using Domain.Rooms.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Rooms
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasColumnType("text");
            builder.Property(e => e.Level).HasColumnType("integer");
            builder.Property(e => e.InMaintenance).HasColumnType("boolean");
            builder.Property(e => e.HasGuest).HasColumnType("boolean");

            // Configuring the owned property Price
            builder.OwnsOne(r => r.Price, price =>
            {
                price.Property(p => p.Value).HasColumnType("decimal(18,2)").IsRequired();
                price.Property(p => p.Currency).HasColumnType("integer").IsRequired(); // enum
            });
        }
    }
}
