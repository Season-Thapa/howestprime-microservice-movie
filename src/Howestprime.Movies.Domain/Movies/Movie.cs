using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies;
public readonly record struct MovieId(Guid Value) : IEntityId;

public sealed class Movie : AggregateRoot<MovieId>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int AgeRating { get; private set; }
    public int ReleaseYear { get; private set; }
    public int Duration { get; private set; }
    public List<Genre> Genres { get; private set; }
    public List<Actor> Actors { get; private set; }
    public string PosterUrl { get; private set; }
    public void Register(
            string title,
            string description,
            int ageRating,
            int releaseYear,
            int duration,
            List<Genre> genres,
            List<Actor> actors,
            string posterUrl
        )
    {
        Asserts.EnsureNotEmpty(title);
        Asserts.EnsureNotEmpty(description);
        Asserts.EnsureGreaterThan(ageRating, 0);
        Asserts.EnsureNotNegative(releaseYear);
        Asserts.EnsureGreaterThan(duration, 0);
        Asserts.EnsureNotEmpty(genres);
        Asserts.EnsureNotEmpty(actors);
        Asserts.EnsureNotEmpty(posterUrl);
        Movie movie = new (
            id  ?? EntityId.New<MovieId>(),
            title,
            description,
            ageRating,
            releaseYear,
            duration,
            genres,
            actors,
            posterUrl
            );
        movie.ValidateState();
        movie.RaiseDomainEvent(new MovieRegistered(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.AgeRating,
            movie.ReleaseYear,
            movie.Duration,
            movie.Genres,
            movie.Actors,
            movie.PosterUrl
            ));


    }

}
