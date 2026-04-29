using System;
using System.Collections.Generic;
using System.Text;
using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
public sealed record Movie(
    Guid Id,
    string Title,
    string Description,
    int AgeRating,
    int ReleaseYear,
    int Duration,
    IReadOnlyList<Genre> Genres,
    IReadOnlyList<Actor> Actors,
    string PosterUrl
);