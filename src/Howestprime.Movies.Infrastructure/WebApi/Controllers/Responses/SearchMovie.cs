using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record SearchMovie
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public int AgeRating { get; init; }
    public int ReleaseYear { get; init; }
    public int Duration { get; init; }
    public IReadOnlyList<string> Genres { get; init; }
    public IReadOnlyList<string> Actors { get; init; }
    public string PosterUrl { get; init; }
}

