using Aornis;
using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent;

public interface IMovieEventRepository : IRepository<MovieEvent, MovieEventId>
{
    //Task<Optional<MovieEvent>> ByShowtimeAndRoomID (DateTime showtime, RoomId roomId);

}