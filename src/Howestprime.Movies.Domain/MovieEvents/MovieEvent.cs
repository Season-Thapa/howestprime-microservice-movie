using Howestprime.Movies.Domain.MovieEvents.Entities;
using Howestprime.Movies.Domain.MovieEvents.Events;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvents;
public readonly record struct MovieEventId(Guid Value) : IEntityId;
public sealed class MovieEvent : AggregateRoot<MovieEventId>
{
    public MovieId MovieId { get; private set; }
    public RoomId RoomId { get; private set; }
    public DateTime ShowTime { get; private set; }
    public int Capacity { get; private set; }
    public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();
    private readonly List<Booking> _bookings = [];
    public int Visitor {get; private set;}

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

    public Booking Book(
        int standardVisitors,
        int discountVisitors,
        List<string> seatNumbers)
    {
        
        Booking booking = Booking.Create(
            standardVisitors,
            discountVisitors,
            seatNumbers
        );
        _bookings.Add(booking);
        RaiseDomainEvent(new BookingOpened(
            booking.Id, 
            BookingStatus.Open, 
            PaymentStatus.Pending, 
            booking.StandardVisitors, 
            booking.DiscountVisitors, 
            booking.SeatNumbers.ToList()));
        return booking;
    }
    public override void ValidateState()
    {
        Asserts.EnsureShowtimeIsAt15hOr19h(ShowTime);
         Asserts.EnsureShowtimeIsInTheFuture(ShowTime);
         Asserts.EnsureGreaterThan(Capacity, 0);
    }
   
}