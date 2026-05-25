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
using Howestprime.Movies.Domain.Movies.ValueObjects;
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
        var fakeRoomRepo = new FakeRoomRepository();
        fakeUow.SetRepository<IMovieEventRepository>(fakeRepo);
        fakeUow.SetRepository<IRoomRepository>(fakeRoomRepo);
        var logger = new Mock<ILogger<BookMovieEvent>>();
        FakeMovieRepository movieRepository = new();
        FakeRoomRepository roomRepository = new();

        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();
        await fakeRoomRepo.Save(room);
        var movieEvent = MovieEvent.Schedule(movie.Id, room.Id, DateTime.Today.AddDays(1).AddHours(15), 10);
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
        var fakeRoomRepo = new FakeRoomRepository();
        fakeUow.SetRepository<IMovieEventRepository>(fakeRepo);
        fakeUow.SetRepository<IRoomRepository>(fakeRoomRepo);
        var logger = new Mock<ILogger<BookMovieEvent>>();
        FakeMovieRepository movieRepository = new();
        FakeRoomRepository roomRepository = new();

        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();
        await fakeRoomRepo.Save(room);
        var movieEvent = MovieEvent.Schedule(movie.Id, room.Id, DateTime.Today.AddDays(1).AddHours(15), 10);
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
        var fakeRoomRepo = new FakeRoomRepository();
        fakeUow.SetRepository<IMovieEventRepository>(fakeRepo);
        fakeUow.SetRepository<IRoomRepository>(fakeRoomRepo);
        var logger = new Mock<ILogger<BookMovieEvent>>();
        FakeMovieRepository movieRepository = new();
        FakeRoomRepository roomRepository = new();

        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();
        await fakeRoomRepo.Save(room);
        var movieEvent = MovieEvent.Schedule(movie.Id, room.Id, DateTime.Today.AddDays(1).AddHours(15), 3);
        await fakeRepo.Save(movieEvent);
        var useCase = new BookMovieEvent(fakeUow, logger.Object);
        var input = new BookMovieEventInput(movieEvent.Id.Value, 2, 2);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.Execute(input));
    }
    private static Room CreateValidRoom()
    {
        return Room.Create(
            "Room 1",
            100
        );
    }

    private static Movie CreateValidMovie()
    {
        MovieTitle title = MovieTitle.Create("Inception");

        string description =
            "A mind-bending thriller about dreams within dreams.";

        ReleaseYear releaseYear = ReleaseYear.Create(2010);

        AgeRating ageRating = AgeRating.Create(13);

        Duration duration = Duration.Create(148);

        PosterUrl posterUrl = PosterUrl.Create(
            "https://example.com/inception-poster.jpg"
        );

        List<Genre> genres =
        [
            Genre.Create("Action"),
            Genre.Create("Sci-Fi"),
            Genre.Create("Thriller")
        ];

        List<Actor> actors =
        [
            Actor.Create("Leonardo DiCaprio"),
            Actor.Create("Joseph Gordon-Levitt"),
            Actor.Create("Elliot Page")
        ];

        return Movie.Register(
            title,
            description,
            releaseYear,
            ageRating,
            duration,
            genres,
            actors,
            posterUrl
        );
    }
}
