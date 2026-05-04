using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace Howestprime.Movies.Application.Movies;


public sealed record RegisterMovieInput(
    MovieTitle Title,
    string Description,
    ReleaseYear ReleaseYear,
    Duration Duration,
    AgeRating AgeRating,
    List<Genre> Genres,
    List<Actor> Actors,
    PosterUrl PosterUrl
);

public sealed class RegisterMovie(
    IUnitOfWork uow
    ) : IUseCase<RegisterMovieInput, Guid>
{
    public async Task<Guid> Execute(RegisterMovieInput input)
    {
        var movie = Movie.Register(
            input.Title,
            input.Description,
            input.ReleaseYear,
            input.Duration,
            input.AgeRating,
            input.Genres,
            input.Actors,
            input.PosterUrl
        );
        await uow.Save<IMovieRepository>(movie);
        await uow.Do();

        return movie.Id.Value;
    }
}

