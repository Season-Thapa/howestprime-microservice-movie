using Howestprime.Movies.Domain.Movies.Events;
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
    public IReadOnlyList<Genre> Genres { get; private set; }
    public IReadOnlyList<Actor> Actors { get; private set; }
    public PosterUrl PosterUrl { get; private set; }
    private Movie(
        MovieId id,
        MovieTitle title,
        string description,
        AgeRating ageRating,
        ReleaseYear releaseYear,
        Duration duration,
        List<Genre> genres,
        List<Actor> actors,
        PosterUrl posterUrl
    ) : base(id)
    {
        Title = title;
        Description = description;
        AgeRating = ageRating;
        ReleaseYear = releaseYear;
        Duration = duration;
        Genres = genres.AsReadOnly();
        Actors = actors.AsReadOnly();
        PosterUrl = posterUrl;
    }
    public static Movie Register(
            MovieTitle title,
            string description,
            AgeRating ageRating,
            ReleaseYear releaseYear,
            Duration duration,
            List<Genre> genres,
            List<Actor> actors,
            PosterUrl posterUrl
        )
    {
        Movie movie = new (
           EntityId.New<MovieId>(),
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
        return movie;


    }
    public override void ValidateState()
    {
        Asserts.EnsureNotEmpty(Title);
        Asserts.EnsureNotEmpty(Description);
        Asserts.EnsureGreaterThan(Genres.Count, 0);
        Asserts.EnsureGreaterThan(Actors.Count, 0);
    }

}
