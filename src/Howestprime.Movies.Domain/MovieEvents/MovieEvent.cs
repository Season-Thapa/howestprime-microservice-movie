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
    public Movie Movie { get; private set; }
    public RoomId RoomId { get; private set; }
    public Room Room { get; private set; }
    public DateTime Showtime { get; private set; }
    public int Capacity { get; private set; }
    public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();
    private readonly List<Booking> _bookings = [];
    public int Visitor {get; private set;}

    private MovieEvent() { }
    public static MovieEvent Schedule(Movie movie, Room room, DateTime showTime, int capacity, MovieEventId? id = null)
    {
        MovieEvent movieEvent = new (
            id ?? EntityId.New<MovieEventId>(),
            movie,
            room,
            showTime,
            capacity
        );
        movieEvent.ValidateState();
        return movieEvent;
    }
    private MovieEvent(MovieEventId id, Movie movie, Room room,DateTime showtime, int capacity) : base(id)
    {
        Movie= movie;
        Room= room;
        MovieId = movie.Id;
        RoomId = room.Id;
        Showtime = showtime;
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
        if (Room == null)
            throw new InvalidOperationException("Room navigation property is not loaded.");
        if (Movie == null)
            throw new InvalidOperationException("Movie navigation property is not loaded.");
        
        _bookings.Add(booking);
        RaiseDomainEvent(new BookingOpened(
            booking.Id, 
            BookingStatus.Open, 
            PaymentStatus.Pending, 
            booking.StandardVisitors, 
            booking.DiscountVisitors,
            MovieId,
            Room.Name,
            booking.SeatNumbers.ToList()));
        return booking;
    }
    public override void ValidateState()
    {
         Asserts.EnsureShowtimeIsAt15hOr19h(Showtime);
         Asserts.EnsureShowtimeIsInTheFuture(Showtime);
         Asserts.EnsureGreaterThan(Capacity, 0);
         Asserts.EnsureNotEmpty(Movie);
         Asserts.EnsureNotEmpty(Room);
    }
   
}