
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.Events;
using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace UnitTests.Domain.Movies;

public sealed class MoviesTests
{
    [Fact]
    public void RegisterMovie_ShouldCreateMovieRegisteredEvent()
    {
        // Arrange
        MovieTitle title = MovieTitle.Create("Inception");
        string description = "A mind-bending thriller about dreams within dreams.";
        ReleaseYear releaseYear = ReleaseYear.Create(2010);
        AgeRating ageRating = AgeRating.Create(13);
        Duration duration = Duration.Create(148);
        PosterUrl posterUrl = PosterUrl.Create("https://example.com/inception-poster.jpg");

        List<Genre> genres = new()
        {
            Genre.Create("Action"),
            Genre.Create("Sci-Fi"),
            Genre.Create("Thriller")
        };

        List<Actor> actors = new()
        {
            Actor.Create("Leonardo DiCaprio"),
            Actor.Create("Joseph Gordon-Levitt"),
            Actor.Create("Elliot Page")
        };
        // Act
        Movie movie = Movie.Register(title, description, releaseYear, ageRating, duration, genres, actors, posterUrl);
        // Assert
        Assert.NotNull(movie);
        Assert.Equal(title, movie.Title);
        Assert.Equal(description, movie.Description);
        Assert.Equal(ageRating, movie.AgeRating);
        Assert.Equal(releaseYear, movie.ReleaseYear);
        Assert.Equal(duration, movie.Duration);
        Assert.Equal(genres.Count, movie.Genres.Count);
        Assert.Equal(actors.Count, movie.Actors.Count);
        Assert.Equal(posterUrl, movie.PosterUrl);
        Assert.Contains(movie.DomainEvents, e => e is MovieRegistered registered &&
        registered.MovieId.Equals(movie.Id) && registered.Title == title);
    }
    [Fact]
    public void Register_ShouldThrow_WhenGenresAreEmpty()
    {
        // Arrange
        var actors = new List<Actor> { Actor.Create("Leonardo DiCaprio") };

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            Movie.Register(
                MovieTitle.Create("Inception"),
                "Description",
                ReleaseYear.Create(2010),
                AgeRating.Create(13),
                Duration.Create(148),
                new List<Genre>(),
                actors,
                PosterUrl.Create("https://example.com/poster.jpg")
            ));
    }
    [Fact]
    public void Register_ShouldThrow_WhenActorsAreEmpty()
    {
        var genres = new List<Genre> { Genre.Create("Action") };

        Assert.Throws<ArgumentException>(() =>
            Movie.Register(
                MovieTitle.Create("Inception"),
                "Description",
                ReleaseYear.Create(2010),
                AgeRating.Create(13),
                Duration.Create(148),
                genres,
                new List<Actor>(),
                PosterUrl.Create("https://example.com/poster.jpg")
            ));
    }
    [Fact]
    public void Register_ShouldThrow_WhenTitleIsEmpty()
    {
        var genres = new List<Genre> { Genre.Create("Action") };
        var actors = new List<Actor> { Actor.Create("Leonardo DiCaprio") };


        Assert.Throws<ArgumentException>(() =>
            Movie.Register(
                MovieTitle.Create(""),
                "Description",
                ReleaseYear.Create(2010),
                AgeRating.Create(13),
                Duration.Create(148),
                genres,
                actors,
                PosterUrl.Create("https://example.com/poster.jpg")
            ));
    }

}