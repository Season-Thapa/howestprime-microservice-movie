using Howestprime.Movies.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Data;

public sealed class MovieDataConfiguration : IEntityTypeConfiguration<MovieData>
{
    public void Configure(EntityTypeBuilder<MovieData> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Title);
        builder.Property(m => m.Description);
        builder.Property(m => m.PosterUrl);
        builder.Property(m => m.ReleaseYear);
        builder.Property(m => m.AgeRating);
        builder.Property(m => m.Duration);
        builder.OwnsMany(m => m.Actors, actorBuilder =>
        {
            actorBuilder.ToJson();
            actorBuilder.Property(a => a.Value);
        });

        builder.OwnsMany(m => m.Genres, genreBuilder =>
        {
            genreBuilder.ToJson();
            genreBuilder.Property(g => g.Value);
        });

    }
}