using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public static class SearchMovieCatalogController
{
    public static async Task<IResult> Invoke(
        [FromServices] IUseCase<SearchMovieCatalogInput, IReadOnlyList<MovieData>> useCase,
        [FromHeader(Name = "x-user-role")] string? userRole,
        [FromQuery] string? title,
        [FromQuery] string? genres)
    {
        if (string.IsNullOrWhiteSpace(userRole) || !userRole.Equals("Manager", StringComparison.OrdinalIgnoreCase))
            return TypedResults.Unauthorized();

        var genreDataList = new List<GenreData>();
        if (!string.IsNullOrWhiteSpace(genres))
        {
            genreDataList = genres
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(g => new GenreData { Value = g.Trim() })
                .ToList();
        }

        var input = new SearchMovieCatalogInput(title ?? string.Empty, genreDataList);

        IReadOnlyList<MovieData> movies;
        try
        {
            movies = await useCase.Execute(input);
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

          


