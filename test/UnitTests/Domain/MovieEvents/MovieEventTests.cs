using System;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using Xunit;

namespace UnitTests.Domain.MovieEvents;

public sealed class MovieEventTests
{
    [Fact]
    public void Schedule_ShouldCreateMovieEvent_WhenValidInput()
    {
        // Arrange
        var movieId = new MovieId(Guid.NewGuid());
        var roomId = new RoomId(Guid.NewGuid());
        var showTime = DateTime.Today.AddDays(1).AddHours(15); // 15:00 tomorrow
        int capacity = 100;

        // Act
        var movieEvent = MovieEvent.Schedule(movieId, roomId, showTime, capacity);

        // Assert
        Assert.Equal(movieId, movieEvent.MovieId);
        Assert.Equal(roomId, movieEvent.RoomId);
        Assert.Equal(showTime, movieEvent.Showtime);
        Assert.Equal(capacity, movieEvent.Capacity);
    }

    [Theory]
    [InlineData(14)]
    [InlineData(16)]
    [InlineData(18)]
    [InlineData(20)]
    public void Schedule_ShouldThrow_WhenShowtimeIsNot15Or19(int hour)
    {
        // Arrange
        var movieId = new MovieId(Guid.NewGuid());
        var roomId = new RoomId(Guid.NewGuid());
        var showTime = DateTime.Today.AddDays(1).AddHours(hour);
        int capacity = 100;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Schedule(movieId, roomId, showTime, capacity));
    }

    [Fact]
    public void Schedule_ShouldThrow_WhenShowtimeIsInThePast()
    {
        // Arrange
        var movieId = new MovieId(Guid.NewGuid());
        var roomId = new RoomId(Guid.NewGuid());
        var showTime = DateTime.Now.AddHours(-1); // Past
        int capacity = 100;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Schedule(movieId, roomId, showTime, capacity));
    }

    [Fact]
    public void Schedule_ShouldThrow_WhenCapacityIsNotGreaterThanZero()
    {
        // Arrange
        var movieId = new MovieId(Guid.NewGuid());
        var roomId = new RoomId(Guid.NewGuid());
        var showTime = DateTime.Today.AddDays(1).AddHours(15);
        int capacity = 0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Schedule(movieId, roomId, showTime, capacity));
    }
}