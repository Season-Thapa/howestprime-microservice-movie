using Aornis;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public sealed class MovieEventRepository(
    DomainDbContext context
    ) : EfCoreGenericRepository<MovieEvent, MovieEventId>(context), IMovieEventRepository
{
    public Task<MovieEvent> ByShowtimeAndRoomID(DateTime showtime, RoomId roomId)
    {
        MovieEvent? movieEvent = _context.MovieEvents
            .FirstOrDefault(m => m.Showtime == showtime && m.RoomId == roomId);

        return Task.FromResult(movieEvent);
    }


 
}