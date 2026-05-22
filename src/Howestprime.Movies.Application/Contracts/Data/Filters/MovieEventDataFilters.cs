using System.Linq.Expressions;

namespace Howestprime.Movies.Application.Contracts.Data.Filters;

public static class MovieEventDataFilters
{
    public static Expression<Func<MovieEventData, bool>> ByTimeRange (DateTime? from, DateTime? to)
    {
        return movieEvent =>
            (!from.HasValue || movieEvent.Showtime.Date >= from.Value)
            && (!to.HasValue || movieEvent.Showtime.Date <= to.Value);
    }
    
}