
using Aornis;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movie = Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses.Movie;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;
public static class GetMovieController
{
    public static async Task<Results<Ok<Movie>, NotFound>> Invoke(
        [FromServices] IMovieRepository movieRepository,
        [FromRoute] Guid movieId)
    {
        var movieOpt = await movieRepository.ById(new MovieId(movieId));
        if (!movieOpt.HasValue)
            return TypedResults.NotFound();

        var movie = movieOpt.Value;
        var response = new Responses.Movie(
            movie.Id.Value,
            movie.Title,
            movie.Description,
            movie.AgeRating,
            movie.ReleaseYear,
            movie.Duration,
            movie.Genres.ToList(),
            movie.Actors.ToList(),
            movie.PosterUrl
        );
        return TypedResults.Ok(response);
    }
}
