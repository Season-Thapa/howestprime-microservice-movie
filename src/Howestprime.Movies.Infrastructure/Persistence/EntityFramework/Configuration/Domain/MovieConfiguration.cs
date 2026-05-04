using System;
using System.Collections.Generic;
using System.Text;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration.Domain;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(m => m.Id);
        
        
        // Configure owned collections as JSON
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
