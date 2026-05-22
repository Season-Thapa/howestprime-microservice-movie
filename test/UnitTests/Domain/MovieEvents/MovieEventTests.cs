using System;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.ValueObjects;
using Howestprime.Movies.Domain.Rooms;
using Xunit;

namespace UnitTests.Domain.MovieEvents;

public sealed class MovieEventTests
{
    [Fact]
    public void Schedule_ShouldCreateMovieEvent_WhenValidInput()
    {
        // Arrange
    
        var showTime = DateTime.Today.AddDays(1).AddHours(15); // 15:00 tomorrow
        int capacity = 100;
      

        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();

        // Act
        var movieEvent = MovieEvent.Schedule(movie, room, showTime, capacity);

        // Assert
        Assert.Equal(movie, movieEvent.Movie);
        Assert.Equal(room, movieEvent.Room);
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

        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();

        var showTime = DateTime.Today.AddDays(1).AddHours(hour);
        int capacity = 100;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Schedule(movie,room, showTime, capacity));
    }

    [Fact]
    public void Schedule_ShouldThrow_WhenShowtimeIsInThePast()
    {
        // Arrange
        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();
        var showTime = DateTime.Now.AddHours(-1); // Past
        int capacity = 100;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Schedule(movie,room, showTime, capacity));
    }

    [Fact]
    public void Schedule_ShouldThrow_WhenCapacityIsNotGreaterThanZero()
    {
        // Arrange
        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();
        var showTime = DateTime.Today.AddDays(1).AddHours(15);
        int capacity = 0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => MovieEvent.Schedule(movie,room, showTime, capacity));
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
