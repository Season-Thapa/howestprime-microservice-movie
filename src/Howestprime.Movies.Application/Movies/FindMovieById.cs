using Aornis;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.Movies;

public record FindMovieByIdInput(Guid movieId);
public class FindMovieById(
    ISearchMovieCatalogQuery searchMovieCatalogQuery) : IUseCase<FindMovieByIdInput, IReadOnlyList<MovieData>>
{
    public Task<IReadOnlyList<MovieData>> Execute(FindMovieByIdInput input)
    {
        return searchMovieCatalogQuery.Fetch(MovieDataFilters.ById(input.movieId));
    }
    
}