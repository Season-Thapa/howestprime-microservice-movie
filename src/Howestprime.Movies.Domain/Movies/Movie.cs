using Howestprime.Movies.Domain.Movies.Events;
using Howestprime.Movies.Domain.Movies.ValueObjects;
using Howestprime.Movies.Domain.Shared;
namespace Howestprime.Movies.Domain.Movies;
public readonly record struct MovieId(Guid Value) : IEntityId;

public sealed class Movie : AggregateRoot<MovieId>
{
    public MovieTitle Title { get; private set; }
    public string Description { get; private set; }
    public AgeRating AgeRating { get; private set; }
    public ReleaseYear ReleaseYear { get; private set; }
    public Duration Duration { get; private set; }
    private readonly List<Genre> _genres = [];
    public IReadOnlyList<Genre> Genres => _genres.AsReadOnly();
    
    private readonly List<Actor> _actors = [];
    public IReadOnlyList<Actor> Actors => _actors.AsReadOnly();
    public PosterUrl PosterUrl { get; private set; }
    private Movie() { }

    private Movie(
        MovieId id,
        MovieTitle title,
        string description,
        ReleaseYear releaseYear,
        AgeRating ageRating,
        Duration duration,
        PosterUrl posterUrl
    ) : base(id)
    {
        Title = title;
        Description = description;
        AgeRating = ageRating;
        ReleaseYear = releaseYear;
        Duration = duration;
        PosterUrl = posterUrl;
    }
    
    public static Movie Register(
        MovieTitle title,
        string description,
        ReleaseYear releaseYear,
        AgeRating ageRating,
        Duration duration,
        List<Genre> genres,
        List<Actor> actors,
        PosterUrl posterUrl
        )
    {
        Movie movie = new Movie(
           EntityId.New<MovieId>(),
            title,
            description,
            releaseYear,
            ageRating,
            duration,
            posterUrl
            );

        movie._genres.AddRange(genres);
        movie._actors.AddRange(actors);

        movie.ValidateState();
        movie.RaiseDomainEvent(new MovieRegistered(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.ReleaseYear,
            movie.AgeRating,
            movie.Duration,
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
