using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.Movies
{
    public sealed record SearchMovieCatalogInput(string Title, IReadOnlyList<GenreData> Genres);
    public sealed class SearchMovieCatalog(
        ISearchMovieCatalogQuery searchMovieCatalogQuery
    ) : IUseCase<SearchMovieCatalogInput, IReadOnlyList<MovieData>>
    {
        public Task<IReadOnlyList<MovieData>> Execute(SearchMovieCatalogInput input)
        {
            return searchMovieCatalogQuery.Fetch(
                MovieDataFilters.ByTitleAndGenres(input.Title, input.Genres)
            );
        }
    }
}


