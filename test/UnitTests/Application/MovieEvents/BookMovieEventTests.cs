using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.MovieEvents.Entities;
using Howestprime.Movies.Shared.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using UnitTests.Application.Mocks;
using Xunit;

namespace UnitTests.Application.MovieEvents;

public sealed class BookMovieEventTests
{
    [Fact]
    public async Task Execute_ShouldBookMovieEvent_WhenInputIsValid()
    {
        // Arrange
        var fakeUow = new FakeUnitOfWork();
        var fakeRepo = new FakeMovieEventRepository();
        fakeUow.SetRepository<IMovieEventRepository>(fakeRepo);
        var logger = new Mock<ILogger<BookMovieEvent>>();

        var movieEvent = MovieEvent.Schedule(new MovieId(Guid.NewGuid()), new RoomId(Guid.NewGuid()), DateTime.Today.AddDays(1).AddHours(15), 10);
        await fakeRepo.Save(movieEvent);

        var useCase = new BookMovieEvent(fakeUow, logger.Object);
        var input = new BookMovieEventInput(movieEvent.Id.Value, 2, 1);

        // Act
        var bookingId = await useCase.Execute(input);

        // Assert
        Assert.NotEqual(default, bookingId);
        Assert.Single(movieEvent.Bookings);
        var booking = movieEvent.Bookings[0];
        Assert.Equal(2, booking.StandardVisitors);
        Assert.Equal(1, booking.DiscountVisitors);
        Assert.Equal(BookingStatus.Open, booking.BookingStatus);
        Assert.Equal(PaymentStatus.Pending, booking.PaymentStatus);
        Assert.Equal(1, fakeUow.SaveCalls);
        Assert.Equal(1, fakeUow.DoCalls);
    }
    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    public async Task Execute_ShouldThrow_WhenVisitorCountInvalid(int standard, int discount)
    {
        // Arrange
        var fakeUow = new FakeUnitOfWork();
        var fakeRepo = new FakeMovieEventRepository();
        fakeUow.SetRepository<IMovieEventRepository>(fakeRepo);
        var logger = new Mock<ILogger<BookMovieEvent>>();
        var movieId = new MovieId(Guid.NewGuid());
        var movieEvent = MovieEvent.Schedule(movieId, new RoomId(Guid.NewGuid()), DateTime.Today.AddDays(1).AddHours(15), 10);
        await fakeRepo.Save(movieEvent);
        var useCase = new BookMovieEvent(fakeUow, logger.Object);
        var input = new BookMovieEventInput(movieEvent.Id.Value, standard, discount);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.Execute(input));
    }

    [Fact]
    public async Task Execute_ShouldThrow_WhenCapacityExceeded()
    {
        // Arrange
        var fakeUow = new FakeUnitOfWork();
        var fakeRepo = new FakeMovieEventRepository();
        fakeUow.SetRepository<IMovieEventRepository>(fakeRepo);
        var logger = new Mock<ILogger<BookMovieEvent>>();
        var movieEvent = MovieEvent.Schedule(new MovieId(Guid.NewGuid()), new RoomId(Guid.NewGuid()), DateTime.Today.AddDays(1).AddHours(15), 3);
        await fakeRepo.Save(movieEvent);
        var useCase = new BookMovieEvent(fakeUow, logger.Object);
        var input = new BookMovieEventInput(movieEvent.Id.Value, 2, 2);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.Execute(input));
    }
}
