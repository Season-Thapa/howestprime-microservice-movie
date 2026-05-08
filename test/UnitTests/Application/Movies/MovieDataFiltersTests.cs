using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;

namespace UnitTests.Application.Movies;

public sealed class MovieDataFiltersTests
{
    [Fact]
    public void ByTitleAndGenres_ShouldMatchTitleCaseInsensitively()
    {
        var movies = new List<MovieData>
        {
            new()
            {
                Title = "Inception",
                Genres = new List<GenreData> { new() { Value = "Action" } }
            },
            new()
            {
                Title = "Tenet",
                Genres = new List<GenreData> { new() { Value = "Sci-Fi" } }
            }
        };

        var filter = MovieDataFilters.ByTitleAndGenres(
            "inception",
            new List<GenreData> { new() { Value = "action" } });

        var result = movies.Where(filter.Compile()).ToList();

        Assert.Single(result);
        Assert.Equal("Inception", result[0].Title);
    }
}

