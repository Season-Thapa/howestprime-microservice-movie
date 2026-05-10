using Howestprime.Movies.Domain.MovieEvent;
using Microsoft.Extensions.Logging;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Seeders;

public class DomainDbSeeder(DomainDbContext context, ILogger<DomainDbSeeder> logger)
{
    public async Task Seed()
    {
        // Seed movies if table is empty
        if (!context.Movies.Any())
        {
            logger.LogInformation("Seeding sample movies...");

            // Sample movies list
            var moviesToSeed = new List<Movie>
            {
                Movie.Register(
                    MovieTitle.Create("Inception"),
                    "A mind-bending thriller about dreams within dreams.",
                    ReleaseYear.Create(2010),
                    AgeRating.Create(13),
                    Duration.Create(148),
                    new List<Genre> { Genre.Create("Action"), Genre.Create("Sci-Fi"), Genre.Create("Thriller") },
                    new List<Actor> { Actor.Create("Leonardo DiCaprio"), Actor.Create("Joseph Gordon-Levitt"), Actor.Create("Elliot Page") },
                    PosterUrl.Create("https://example.com/inception-poster.jpg")
                ),
                Movie.Register(
                    MovieTitle.Create("The Matrix"),
                    "A computer hacker learns about the true nature of his reality.",
                    ReleaseYear.Create(1999),
                    AgeRating.Create(16),
                    Duration.Create(136),
                    new List<Genre> { Genre.Create("Action"), Genre.Create("Sci-Fi") },
                    new List<Actor> { Actor.Create("Keanu Reeves"), Actor.Create("Laurence Fishburne"), Actor.Create("Carrie-Anne Moss") },
                    PosterUrl.Create("https://example.com/matrix-poster.jpg")
                ),
                Movie.Register(
                    MovieTitle.Create("Interstellar"),
                    "A team of astronauts travel through a wormhole to save humanity.",
                    ReleaseYear.Create(2014),
                    AgeRating.Create(12),
                    Duration.Create(169),
                    new List<Genre> { Genre.Create("Science Fiction"), Genre.Create("Drama"), Genre.Create("Adventure") },
                    new List<Actor> { Actor.Create("Matthew McConaughey"), Actor.Create("Anne Hathaway"), Actor.Create("Jessica Chastain") },
                    PosterUrl.Create("https://example.com/interstellar-poster.jpg")
                )
            };

            await context.Movies.AddRangeAsync(moviesToSeed);
            await context.SaveChangesAsync();
        }

        // Seed rooms if table is empty
        if (!context.Rooms.Any())
        {
            var roomsToSeed = new List<Room>
            {
                Room.Create("Blue Room", 100, EntityId.New<RoomId>(Guid.Parse("019d059e-d220-71db-8a1a-ec7569492999"))),
                Room.Create("Yellow Room", 80, EntityId.New<RoomId>(Guid.Parse("019d059e-d220-75fe-b936-0a97cd75216e")))
            };
            await context.Rooms.AddRangeAsync(roomsToSeed);
            await context.SaveChangesAsync();
        }

        // Seed movie events if table is empty
        if (!context.MovieEvents.Any())
        {
            var movies = context.Movies.ToList();
            var rooms = context.Rooms.ToList();

         
                var movieEventsToSeed = new List<MovieEvent>
                {
                    MovieEvent.Schedule(
                        movies[0].Id, 
                        rooms[0].Id,
                        // MUST use UtcNow.Date to assure DateTimeKind.Utc, required by Npgsql (PostgreSQL)
                        DateTime.UtcNow.Date.AddDays(1).AddHours(19),
                        rooms[0].Capacity
                    ),
                    MovieEvent.Schedule(
                        movies[1].Id,  
                        rooms[1].Id,
                        DateTime.UtcNow.Date.AddDays(1).AddHours(19),
                        rooms[1].Capacity
                    )
                };

                await context.MovieEvents.AddRangeAsync(movieEventsToSeed);
                await context.SaveChangesAsync();
            
        }

        logger.LogInformation("Samples seeded successfully!");
    }
}
