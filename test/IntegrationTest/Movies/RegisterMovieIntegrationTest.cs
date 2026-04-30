using Howestprime.Movies.Domain.Movies.ValueObjects;
using IntegrationTest.Shared;
using Xunit;

namespace IntegrationTest.Movies;
[Collection("MovieApi")]
public sealed class RegisterMovieIntegrationTest(
    MovieApiFixture fixture
    )
{
    private readonly MovieApiClient _client = fixture.Client;
        [Fact]
        public async Task RegisterMovie_ShouldSucceed()
        {
            // Arrange
            MovieTitle title = MovieTitle.Create("The Matrix");
            ReleaseYear releaseYear = ReleaseYear.Create(1999);
            AgeRating ageRating = AgeRating.Create(16);
            var actors = new List<Actor> { Actor.Create("Keanu Reeves"), Actor.Create("Laurence Fishburne") };
            var genres = new List<Genre> { Genre.Create("Action"), Genre.Create("Action") };
            string description = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.";
            Duration duration = Duration.Create(136);
            PosterUrl posterUrl = PosterUrl.Create("https://example.com/matrix-poster.jpg");

            // Act
            Guid movieId = await _client.RegisterMovieAsync(title, description, ageRating, releaseYear, duration, genres, actors, posterUrl);
            var movie = await _client.GetMovieAsync(movieId);
            // Assert 
            Assert.NotNull(movie);
            Assert.Equal(description, movie.Description);
            Assert.Equal(ageRating, movie.AgeRating);
            Assert.Equal(releaseYear, movie.ReleaseYear);
            Assert.Equal(duration, movie.Duration);
            Assert.Equal(posterUrl, movie.PosterUrl);
            Assert.Equal(actors, movie.Actors);
            Assert.Equal(genres, movie.Genres);
            Assert.Equal(movieId, movie.Id);
        }
}