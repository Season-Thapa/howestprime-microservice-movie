using Howestprime.Movies.Domain.MovieEvents.Entities;
using Howestprime.Movies.Domain.Movies;

namespace Howestprime.Movies.Domain.MovieEvents.Events;

public class BookingOpened(
    BookingId  BookingId,
    BookingStatus status,
    PaymentStatus paymentStatus,
    int StandardVisitors,
    int DiscountVisitors,
    MovieId MovieId,
    string room,
    List<string> _seatNumbers) : MovieEventDomainEvent(nameof(BookingOpened))

{
    public BookingId BookingId { get; } = BookingId;
    public BookingStatus Status { get; } = status;
    public PaymentStatus PaymentStatus { get; } = paymentStatus;
    public int StandardVisitors { get; } = StandardVisitors;
    public int DiscountVisitors { get; } = DiscountVisitors;
    public MovieId MovieId { get; } = MovieId;
    public string Room { get; } = room;
    public IReadOnlyList<string> SeatNumbers => _seatNumbers.AsReadOnly();
}