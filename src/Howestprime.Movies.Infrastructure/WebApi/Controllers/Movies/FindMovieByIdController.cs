using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Howestprime.Movies.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public record FindMovieByIdRequest(
    [FromRoute]Guid MovieId,
    [FromServices] IUseCase<FindMovieByIdInput, IReadOnlyList<MovieData>> UseCase);
public static class FindMovieByIdController
{
    public static async Task<IResult> Invoke(
        [AsParameters] FindMovieByIdRequest request)
    {
        FindMovieByIdInput input = new (request.MovieId);
     IReadOnlyList<MovieData> movies;
     try
     {
        movies = await request.UseCase.Execute(input);
     }
     catch (Exception ex)
     {
         return TypedResults.Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);

     }
     var responses = movies.Select(m => new SearchMovie
     {
         Id = m.Id,
         Title = m.Title,
         Description = m.Description,
         AgeRating = m.AgeRating,
         ReleaseYear = m.ReleaseYear,
         Duration = m.Duration,
         Genres = m.Genres?.Select(g => g.Value).ToList() ?? [],
         Actors = m.Actors?.Select(a => a.Value).ToList() ?? [],
         PosterUrl = m.PosterUrl
     }).ToList();
     var collection = new SearchMovieCollection
     {
         Data = responses
     };

        return TypedResults.Ok(collection);
    }

}
