using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace Howestprime.Movies.Application.Movies;


public sealed record RegisterMovieInput(
    string Title,
    string Description,
    int ReleaseYear,
    int AgeRating,
    int Duration,
    List<string> Genres,
    List<string> Actors,
    string PosterUrl
);

public sealed class RegisterMovie(
    IUnitOfWork uow
    ) : IUseCase<RegisterMovieInput, Guid>
{
    public async Task<Guid> Execute(RegisterMovieInput input)
    {
        var movie = Movie.Register(
            MovieTitle.Create(input.Title),
            input.Description,
            ReleaseYear.Create(input.ReleaseYear),
            AgeRating.Create(input.AgeRating),
            Duration.Create(input.Duration),
            input.Genres.Select(Genre.Create).ToList(),
            input.Actors.Select(Actor.Create).ToList(),
            PosterUrl.Create(input.PosterUrl)
        );
        await uow.Save<IMovieRepository>(movie);
        await uow.Do();

        return movie.Id.Value;
    }
}

