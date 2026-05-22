using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Data.Filters;
using Howestprime.Movies.Application.Contracts.Ports;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record SearchMovieEventsInTimeRangeInput(DateTime from, DateTime to);
public sealed class SearchMovieEventsInTimeRange(
    IMovieEventQuery movieEventQuery) : IUseCase<SearchMovieEventsInTimeRangeInput, IReadOnlyList<MovieEventData>>
{
    public Task<IReadOnlyList<MovieEventData>> Execute(SearchMovieEventsInTimeRangeInput input)
    {
        return movieEventQuery.Fetch(
            MovieEventDataFilters.ByTimeRange(input.from, input.to));
    }
}