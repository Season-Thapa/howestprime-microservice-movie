using Howestprime.Movies.Domain.Movies.ValueObjects;
using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies;
public readonly record struct MovieId(Guid Value) : IEntityId;

public sealed class Movie : AggregateRoot<MovieId>
{
    public MovieTitle Title { get; private set; }
    public string Description { get; private set; }
    public AgeRating AgeRating { get; private set; }
    public ReleaseYear ReleaseYear { get; private set; }
    public Duration Duration { get; private set; }
    public Genres Genres { get; private set; }
    public Actors Actors { get; private set; }
    public PosterUrl PosterUrl { get; private set; }
    public void Register(
            MovieTitle title,
            string description,
            AgeRating ageRating,
            ReleaseYear releaseYear,
            Duration duration,
            List<Genres> genres,
            List<Actor> actors,
            PosterUrl posterUrl
        )
    {
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
