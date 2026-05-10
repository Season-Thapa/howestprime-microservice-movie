using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace Howestprime.Movies.Application.Contracts.Data;

public sealed record MovieData
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ReleaseYear { get; set; }
    public IReadOnlyList<GenreData> Genres { get; set; } = new List<GenreData>();
    public IReadOnlyList<ActorData> Actors { get; set; } = new List<ActorData>();
    public int AgeRating { get; set; }
    public int Duration { get; set; }
    public string PosterUrl { get; set; }
}