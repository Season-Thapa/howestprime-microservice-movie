using System;
using System.Collections.Generic;
using System.Text;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Domain;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Title).IsRequired();
        builder.Property(m => m.Description).IsRequired();
        builder.Property(m => m.AgeRating).IsRequired();
        builder.Property(m => m.ReleaseYear).IsRequired();
        builder.Property(m => m.Duration).IsRequired();
        builder.Property(m => m.PosterUrl).IsRequired();
        builder.OwnsMany(m => m.Actors, actorBuilder =>      
        {
            actorBuilder.ToJson();
            actorBuilder.Property(a => a.Name);
        });

        builder.OwnsMany(m => m.Genres, genreBuilder =>
        {
            genreBuilder.ToJson();
            genreBuilder.Property(g => g.Name);
        });

    }
}
