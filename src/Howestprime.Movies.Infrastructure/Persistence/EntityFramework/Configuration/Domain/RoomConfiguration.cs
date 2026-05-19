using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Domain;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(m => m.Id);
        builder.Property(room => room.Name).IsRequired();
        builder.Property(room => room.Capacity).IsRequired();
    }   
}