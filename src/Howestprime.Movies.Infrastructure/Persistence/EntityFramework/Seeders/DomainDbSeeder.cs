using Microsoft.Extensions.Logging;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.ValueObjects;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Seeders;

public class DomainDbSeeder(DomainDbContext context, ILogger<DomainDbSeeder> logger)
{
    public async Task Seed()
    {
        // Only seed if no movies exist
        if (context.Movies.Any())
            return;

        logger.LogInformation("Seeding sample movies...");

        // Sample movies list
        var movies = new List<Movie>
        {
            Movie.Register(
                MovieTitle.Create("Inception"),
                "A mind-bending thriller about dreams within dreams.",
                ReleaseYear.Create(2010),
                Duration.Create(148),
                AgeRating.Create(13),
                new List<Genre> { Genre.Create("Action"), Genre.Create("Sci-Fi"), Genre.Create("Thriller") },
                new List<Actor> { Actor.Create("Leonardo DiCaprio"), Actor.Create("Joseph Gordon-Levitt"), Actor.Create("Elliot Page") },
                PosterUrl.Create("https://example.com/inception-poster.jpg")
            ),
            Movie.Register(
                MovieTitle.Create("The Matrix"),
                "A computer hacker learns about the true nature of his reality.",
                ReleaseYear.Create(1999),
                Duration.Create(136),
                AgeRating.Create(16),
                new List<Genre> { Genre.Create("Action"), Genre.Create("Sci-Fi") },
                new List<Actor> { Actor.Create("Keanu Reeves"), Actor.Create("Laurence Fishburne"), Actor.Create("Carrie-Anne Moss") },
                PosterUrl.Create("https://example.com/matrix-poster.jpg")
            ),
            Movie.Register(
                MovieTitle.Create("Interstellar"),
                "A team of astronauts travel through a wormhole to save humanity.",
                ReleaseYear.Create(2014),
                Duration.Create(169),
                AgeRating.Create(12),
                new List<Genre> { Genre.Create("Science Fiction"), Genre.Create("Drama"), Genre.Create("Adventure") },
                new List<Actor> { Actor.Create("Matthew McConaughey"), Actor.Create("Anne Hathaway"), Actor.Create("Jessica Chastain") },
                PosterUrl.Create("https://example.com/interstellar-poster.jpg")
            )
        };

        // Add movies to context
        foreach (var movie in movies)
        {
            await context.Movies.AddAsync(movie);
        }

        // Save changes
        await context.SaveChangesAsync();
        
        logger.LogInformation("Sample movies seeded successfully!");
    }
}
