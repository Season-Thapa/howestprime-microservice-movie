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
 
   PosterUrl posterUrl
) : MovieDomainEvent(nameof(MovieRegistered))
{
    public MovieId MovieId { get; init; } = movieId;
    public MovieTitle Title { get; init; } = title;
    public string Description { get; init; } = description;
    public AgeRating AgeRating { get; init; } = ageRating;
    public ReleaseYear ReleaseYear { get; init; } = releaseYear;
    public Duration Duration { get; init; } = duration;

    public PosterUrl PosterUrl { get; init; } = posterUrl;
}