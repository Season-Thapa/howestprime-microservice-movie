namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record SearchMovie
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public int AgeRating { get; init; }
    public int ReleaseYear { get; init; }
    public int Duration { get; init; }
    public IReadOnlyList<string> Genres { get; init; } = [];
    public IReadOnlyList<string> Actors { get; init; } = [];
    public string PosterUrl { get; init; } = null!;
}

