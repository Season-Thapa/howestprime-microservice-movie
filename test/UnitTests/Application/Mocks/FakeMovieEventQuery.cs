using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;

namespace UnitTests.Application.Mocks;

public sealed class FakeMovieEventQuery(IReadOnlyList<MovieEventData> result) : IMovieEventQuery
{
    private readonly IReadOnlyList<MovieEventData> _result = result;

    public Task<IReadOnlyList<MovieEventData>> Fetch(System.Linq.Expressions.Expression<Func<MovieEventData, bool>> movieEventFilter)
    {
        var predicate = movieEventFilter.Compile();
        var filtered = new List<MovieEventData>();
        foreach (var ev in _result)
        {
            if (predicate(ev))
            {
                filtered.Add(ev);
            }
        }

        return Task.FromResult((IReadOnlyList<MovieEventData>)filtered);
    }
}