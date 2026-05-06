using System;
using System.Collections.Generic;
using System.Text;
using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
public sealed record Movie(
    Guid Id,
    MovieTitle Title,
    string Description,
    AgeRating AgeRating,
    ReleaseYear ReleaseYear,
    Duration Duration,
    List<Genre> Genres,
    List<Actor> Actors,
    PosterUrl PosterUrl
);