using Aornis;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;

namespace UnitTests.Application.Mocks
{
    public sealed class FakeMovieEventRepository : IMovieEventRepository
    {
        private readonly HashSet<MovieEvent> _movieEvents = [];
        public Task<Optional<MovieEvent>> ById(MovieEventId id)
        {
            return Task.FromResult(
           Optional.Of(_movieEvents.FirstOrDefault(m => m.Id.Equals(id)))
       );
        }

        public Task<MovieEvent> ByShowtimeAndRoomID(DateTime showtime, RoomId roomId)
        {
            return Task.FromResult(
                _movieEvents.FirstOrDefault(m => m.Showtime.Equals(showtime) && m.RoomId.Equals(roomId))
            );
        }

        public Task<MovieEvent?> GetById(MovieEventId id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(MovieEventId id)
        {
            return Task.FromResult(
                _movieEvents.Any(movieEvent => movieEvent.Id.Equals(id))
            );
        }

        public Task Remove(MovieEvent aggregateRoot)
        {
            return Task.FromResult(
                _movieEvents.Remove(aggregateRoot)
            );
        }

        public Task Save(MovieEvent aggregateRoot)
        {
            _movieEvents.Add(aggregateRoot);
            return Task.CompletedTask;
        }
    }
}