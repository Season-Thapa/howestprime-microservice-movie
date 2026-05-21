using Howestprime.Movies.Domain.MovieEvents.Entities;
using Xunit;
using System;
using System.Collections.Generic;

namespace UnitTests.Domain.MovieEvents;

public sealed class BookingTests
{
    [Fact]
    public void Create_ShouldInitializeBookingWithCorrectValues()
    {
        // Arrange
        int standardVisitors = 2;
        int discountVisitors = 1;
        var seatNumbers = new List<string> { "A1", "A2", "A3" };

        // Act
        var booking = Booking.Create(standardVisitors, discountVisitors, seatNumbers);

        // Assert
        Assert.NotNull(booking);
        Assert.Equal(standardVisitors, booking.StandardVisitors);
        Assert.Equal(discountVisitors, booking.DiscountVisitors);
        Assert.Equal(BookingStatus.Open, booking.BookingStatus);
        Assert.Equal(PaymentStatus.Pending, booking.PaymentStatus);
        Assert.Equal(seatNumbers, booking.SeatNumbers);
        Assert.NotEqual(default, booking.Id);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(-2, -3)]
    public void Create_ShouldThrow_WhenVisitorsNegative(int standardVisitors, int discountVisitors)
    {
        // Arrange
        var seatNumbers = new List<string> { "A1" };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Booking.Create(standardVisitors, discountVisitors, seatNumbers));
    }
}
