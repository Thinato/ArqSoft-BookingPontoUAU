using Domain.Bookings.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Bookings
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.PlacedAt)
                    .HasColumnType("DateTime");

            builder.Property(b => b.Start)
                    .HasColumnType("DateTime");

            builder.Property(b => b.End)
                    .HasColumnType("DateTime");

            builder.Property(b => b.CurrentStatus)
                    .HasColumnName("status")
                    .HasColumnType("SmallInt");
        
            builder.HasOne(b => b.Guest)
                    .WithMany(g => g.Bookings)
                    .HasForeignKey("guest_id")
                    .HasPrincipalKey(g => g.Id);
            
            builder.HasOne(b => b.Room)
                    .WithMany(r => r.Bookings)
                    .HasForeignKey("room_id")
                    .HasPrincipalKey(r => r.Id);
        }
    }
}