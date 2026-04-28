using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public record RegisterMovieRequest(
    [FromBody] RegisterMovieBody Body,
    [FromService] IUseCase<RegisterMovieInput, Guid> UseCase);

public static class RegisterMovieController
{
    public static async Task<Results<Created, BadRequest>> Invoke(
        [AsParameters]RegisterMovieRequest request)
    {
        RegisterMovieInput input = new RegisterMovieInput(
            request.Body.Title,
            request.Body.Description,
            request.Body.ReleaseDate,
            request.Body.Genre,
            request.Body.Actor,
            request.Body.Duration,
            request.Body.AgeRating,
            request.Body.PosterUrl);
        Guid movieId = await request.UseCase.Execute(input);
        return TypedResults.Created($"/movies/{movieId}");
    }
}
public record RegisterMovieBody(
    [Required]string Title,
    [Required] string Description,
    [Required] int ReleaseDate,
    [Required] List<Genre> Genre,
    [Required] List<Actor> Actor,
    [Required] int Duration,
    [Required] int AgeRating,
    [Required] string PosterUrl
);
