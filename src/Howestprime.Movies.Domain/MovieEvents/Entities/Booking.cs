using Howestprime.Movies.Domain.Shared;

namespace Howestprime.Movies.Domain.MovieEvents.Entities;

public enum BookingStatus
{
   Open,
   Closed,
   
}
public enum PaymentStatus
{
    Pending,
    Success,
    Failed,
}
public readonly record struct BookingId(Guid Value) : IEntityId;
public sealed class Booking : Entity<BookingId>
{

    public BookingStatus BookingStatus { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public int StandardVisitors { get; private set; }
    public int DiscountVisitors { get; private set; }
    public IReadOnlyList<string> SeatNumbers => _seatNumbers.AsReadOnly();
    private  List<string> _seatNumbers = [];
    public Booking() { }
    private Booking(BookingId id, BookingStatus bookingStatus, PaymentStatus paymentStatus, int standardVisitors, int discountVisitors, List<string> seatNumbers) : base(id)
    {
        BookingStatus = bookingStatus;
        PaymentStatus = paymentStatus;
        StandardVisitors = standardVisitors;
        DiscountVisitors = discountVisitors;
        _seatNumbers = seatNumbers;
    }
    

    public static Booking Create( int standardVisitors,
        int discountVisitors,  List<string> seatNumbers)
    {
        Booking booking = new (
            EntityId.New<BookingId>(),
            BookingStatus.Open,
            PaymentStatus.Pending,
            standardVisitors,
            discountVisitors,
            seatNumbers
        );
        booking.ValidateState();
        return booking;
    }

    public override void ValidateState()
    {
        Asserts.EnsureEnumDefined(BookingStatus);
        Asserts.EnsureEnumDefined(PaymentStatus);

        Asserts.EnsureNotNegative(StandardVisitors);
        Asserts.EnsureNotNegative(DiscountVisitors);
        

    }
}