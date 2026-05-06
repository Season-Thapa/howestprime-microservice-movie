using System.Linq.Expressions;
using System.Linq;
using Howestprime.Movies.Application.Contracts.Data;

namespace Howestprime.Movies.Application.Contracts.Data.Filters;

public static class MovieDataFilters
{
    public static Expression<Func<MovieData, bool>> ByTitleAndGenres(string title, IReadOnlyList<GenreData> genres)
    {
        var genreValues = genres?.Select(g => g.Value).ToList() ?? new List<string>();

        return movie =>
            (string.IsNullOrEmpty(title) || movie.Title.Contains(title))
            && (genreValues.Count == 0 || movie.Genres.Any(g => genreValues.Contains(g.Value)));
    }
}
