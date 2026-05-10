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
            string title = "The Matrix";
            int releaseYear = 1999;
            int ageRating = 16;
            var actors = new List<string> { "Keanu Reeves", "Laurence Fishburne" };
            var genres = new List<string> { "Action", "Action" };
            string description = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.";
            int duration = 136;
            string posterUrl = "https://example.com/matrix-poster.jpg";

            // Act
            Guid movieId = await _client.RegisterMovieAsync(title, description, ageRating, releaseYear, duration, genres, actors, posterUrl);
            var movie = await _client.GetMovieAsync(movieId);
            // Assert 
            Assert.NotNull(movie);
            Assert.Equal(movie, movie);
            Assert.Equal(title, movie.Title);
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