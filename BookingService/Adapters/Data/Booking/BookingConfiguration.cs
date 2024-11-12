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
                    .HasColumnType("TimeStamp");

            builder.Property(b => b.Start)
                    .HasColumnType("TimeStamp");

            builder.Property(b => b.End)
                    .HasColumnType("TimeStamp");

            builder.Property(b => b.Status)
                    .HasColumnName("status")
                    .HasColumnType("SmallInt");

            builder.Ignore(b => b.CurrentStatus);
        
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