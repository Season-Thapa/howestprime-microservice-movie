using Howestprime.Movies.Domain.MovieEvents.Entities;
using Howestprime.Movies.Domain.Movies;

namespace Howestprime.Movies.Domain.MovieEvents.Events;

public class BookingOpened(
    string  BookingId,
    BookingStatus status,
    PaymentStatus paymentStatus,
    int StandardVisitors,
    int DiscountVisitors,
    string  MovieId,
    string room,
    string showtime ,
    List<string> _seatNumbers) : MovieEventDomainEvent(nameof(BookingOpened))

{
    public string  BookingId { get; } = BookingId;
    public BookingStatus Status { get; } = status;
    public PaymentStatus PaymentStatus { get; } = paymentStatus;
    public int StandardVisitors { get; } = StandardVisitors;
    public int DiscountVisitors { get; } = DiscountVisitors;
    public string  MovieId { get; } = MovieId;
    public string Room { get; } = room;
    public string Showtime { get; } = showtime;
    public IReadOnlyList<string> SeatNumbers => _seatNumbers.AsReadOnly();
}