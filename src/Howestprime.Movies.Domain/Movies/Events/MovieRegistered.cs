using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.Events;

public sealed class MovieRegistered
(
   MovieId movieId,
   string title,
   string description,
   int ageRating,
   int releaseYear,
   int duration,
   List<Genre> genres,
   List<Actor> actors,
   string posterUrl
) : MovieDomainEvent(nameof(MovieRegistered))
{
    public MovieId MovieId { get; init; } = movieId;
    public string Title { get; init; } = title;
    public string Description { get; init; } = description;
    public int AgeRating { get; init; } = ageRating;
    public int ReleaseYear { get; init; } = releaseYear;
    public int Duration { get; init; } = duration;
    public List<Genre> Genres { get; init; } = genres;
    public List<Actor> Actors { get; init; } = actors;
    public string PosterUrl { get; init; } = posterUrl;
}