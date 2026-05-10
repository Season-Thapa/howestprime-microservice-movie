using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvent;
public readonly record struct MovieEventId(Guid Value) : IEntityId;
public sealed class MovieEvent : AggregateRoot<MovieEventId>
{
    public MovieId MovieId { get; private set; }
    public RoomId RoomId { get; private set; }
    public DateTime ShowTime { get; private set; }
    public int Capacity { get; private set; }

    public static MovieEvent Schedule(MovieId movieId, RoomId roomId, DateTime showTime, int capacity, MovieEventId? id = null)
    {
        MovieEvent movieEvent = new (
            id ?? EntityId.New<MovieEventId>(),
            movieId,
            roomId,
            showTime,
            capacity
        );
        movieEvent.ValidateState();
        return movieEvent;
    }
    private MovieEvent(MovieEventId id, MovieId movieId, RoomId roomId,DateTime showTime, int capacity) : base(id)
    {
        MovieId = movieId;
        RoomId = roomId;
        ShowTime = showTime;
        Capacity = capacity;
    }
    public override void ValidateState()
    {
        Asserts.EnsureShowtimeIsAt15hOr19h(ShowTime);
         Asserts.EnsureShowtimeIsInTheFuture(ShowTime);
         Asserts.EnsureGreaterThan(Capacity, 0);
    }
}