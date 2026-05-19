using Howestprime.Movies.Domain.Movies.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.Events;

public sealed class MovieRegistered
(
   MovieId movieId,
   MovieTitle title,
   string description,
   ReleaseYear releaseYear,
   AgeRating ageRating,
   Duration duration,
   List<Genre> genres,
   List<Actor> actors,
   PosterUrl posterUrl
) : MovieDomainEvent(nameof(MovieRegistered))
{
    public MovieId MovieId { get; private init; } = movieId;
    public MovieTitle Title { get; private init; } = title;
    public string Description { get; private init; } = description;
    public AgeRating AgeRating { get; private init; } = ageRating;
    public ReleaseYear ReleaseYear { get; private init; } = releaseYear;
    public Duration Duration { get; private init; } = duration;
    public List<Genre> Genres { get; private init; } = genres;
    public List<Actor> Actors { get; private init; } = actors;

    public PosterUrl PosterUrl { get; private init; } = posterUrl;
}