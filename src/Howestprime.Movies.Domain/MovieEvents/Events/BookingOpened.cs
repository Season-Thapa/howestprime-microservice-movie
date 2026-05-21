using Howestprime.Movies.Domain.MovieEvents.Entities;

namespace Howestprime.Movies.Domain.MovieEvents.Events;

public class BookingOpened(
    BookingId  BookingId,
    BookingStatus status,
    PaymentStatus paymentStatus,
    int StandardVisitors,
    int DiscountVisitors,
    List<string> _seatNumbers) : MovieEventDomainEvent(nameof(BookingOpened))

{
    public BookingId BookingId { get; } = BookingId;
    public BookingStatus Status { get; } = status;
    public PaymentStatus PaymentStatus { get; } = paymentStatus;
    public int StandardVisitors { get; } = StandardVisitors;
    public int DiscountVisitors { get; } = DiscountVisitors;
    public IReadOnlyList<string> SeatNumbers => _seatNumbers.AsReadOnly();
}