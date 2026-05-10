using System.Linq.Expressions;
using Aornis;

namespace Howestprime.Movies.Application.Contracts.Data.Filters;

public static class MovieDataFilters
{
    public static Expression<Func<MovieData, bool>> ByTitleAndGenres(string title, IReadOnlyList<GenreData> genres)
    {
        var normalizedTitle = title.Trim().ToLowerInvariant();
        var genreValues = genres.Select(g => g.Value.Trim().ToLowerInvariant()).ToList();

        return movie =>
            (string.IsNullOrEmpty(normalizedTitle) || (movie.Title != null && movie.Title.ToLower().Contains(normalizedTitle)))
            && (genreValues.Count == 0 || (movie.Genres != null && movie.Genres.Any(g => g.Value != null && genreValues.Contains(g.Value.ToLower()))));
    }

    public static Expression<Func<MovieData, bool>> ById(Guid movieId)
    {
        return movie => movie.Id == movieId;
    }


}
