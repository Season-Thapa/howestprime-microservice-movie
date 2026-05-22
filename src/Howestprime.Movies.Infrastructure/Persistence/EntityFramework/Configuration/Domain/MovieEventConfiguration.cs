using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Domain;

public class MovieEventConfiguration : IEntityTypeConfiguration<MovieEvent>
{
    public void Configure(EntityTypeBuilder<MovieEvent> builder)
    {
        builder.ToTable("MovieEvents");
        builder.HasKey(m => m.Id);
        builder.HasOne(me => me.Movie)
            .WithMany()
            .HasForeignKey("MovieId")    
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(me => me.Room)
            .WithMany()
            .HasForeignKey("RoomId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(movieEvent => movieEvent.Showtime )
            .IsRequired();
        builder.Property(movieEvent => movieEvent.Capacity ).IsRequired();
        builder.OwnsMany(movieEvent => movieEvent.Bookings, ownedBuilder =>
        {
            ownedBuilder.ToTable("Bookings");
            ownedBuilder.WithOwner().HasForeignKey();
            ownedBuilder.HasKey(b => b.Id);
            ownedBuilder.Property(b => b.StandardVisitors).IsRequired();
            ownedBuilder.Property(b => b.DiscountVisitors).IsRequired();
            ownedBuilder.Property(b => b.PaymentStatus).IsRequired();
            ownedBuilder.Property(b => b.BookingStatus).IsRequired();
            ownedBuilder.Property(b => b.SeatNumbers)
                .HasConversion(
                    seatNumbers => string.Join(',', seatNumbers),
                    value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                .Metadata.SetValueComparer(
                    new ValueComparer<IReadOnlyList<string>>(
                        (c1, c2) => c1!.SequenceEqual(c2!),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));

        });
    }

 
}