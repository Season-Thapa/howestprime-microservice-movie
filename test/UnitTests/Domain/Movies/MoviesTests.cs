using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Domain.Shared;

namespace UnitTests.Domain.Movies;

public sealed class MoviesTests
{
    [Fact]
    public void RegisterMovie_ShouldCreateMovieRegisteredEvent()
    {
        // Arrange
        MovieId movieId = MovieId.Create();
        MovieTitle title = MovieTitle.Create("Inception");
        string description = "A mind-bending thriller about dreams within dreams.";
        AgeRating ageRating = AgeRating.Create(13);
        ReleaseYear releaseYear = ReleaseYear.Create(2010);
        Duration duration = Duration.Create(TimeSpan.FromMinutes(148));
        Genres genres = Genres.Create(new List<string> { "Action", "Sci-Fi", "Thriller" });
        Actors actors = Actors.Create(new List<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt", "Ellen Page" });
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/inception-poster.jpg");
        // Act
        Movie movie = Movie.Register(movieId, title, description, ageRating, releaseYear, duration, genres, actors, posterUrl);
        // Assert
        Assert.NotNull(movie);
        Assert.Equal(movieId, movie.Id);
        Assert.Equal(title, movie.Title);
        Assert.Equal(description, movie.Description);
        Assert.Equal(ageRating, movie.AgeRating);
        Assert.Equal(releaseYear, movie.ReleaseYear);
        Assert.Equal(duration, movie.Duration);
        Assert.Equal(genres, movie.Genres);
        Assert.Equal(actors, movie.Actors);
        Assert.Equal(posterUrl, movie.PosterUrl);
        // Verify that the MovieRegistered event was raised
   
        MovieRegistered? @event = movie.DomainEvents.OfType<MovieRegistered>().FirstOrDefault();

        Assert.NotNull(@event);
        Assert.Equal(movieId, @event.MovieId);
        Assert.Equal(title, @event.Title);
        Assert.Equal(description, @event.Description);
        Assert.Equal(ageRating, @event.AgeRating);
        Assert.Equal(releaseYear, @event.ReleaseYear);
        Assert.Equal(duration, @event.Duration);
        Assert.Equal(genres, @event.Genres);
        Assert.Equal(actors, @event.Actors);
        Assert.Equal(posterUrl, @event.PosterUrl);
        Assert.Contains(movie.DomainEvents, e => e is MovieRegistered registered &&
        registered.MovieId.Equals(movieId));
    }
}
