using System.Net.Http.Json;
using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace IntegrationTest.Shared;

public sealed class MovieApiClient(HttpClient client)
{
        public async Task<Guid> RegisterMovieAsync(MovieTitle title,string description,AgeRating ageRating,ReleaseYear releaseYear,Duration duration,List<Genre> genres,List<Actor> actors,PosterUrl posterUrl)
        {
            var body = new
            {
                Title = title,
                Description = description,
                ReleaseYear = releaseYear,
                Genres = genres,
                Actors = actors,
                Duration = duration,
                AgeRating = ageRating,
                PosterUrl = posterUrl
            };
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/movie-catalog", body, TestUtils.JsonOptions);
            response.EnsureSuccessStatusCode();
            return TestUtils.ExtractIdFromLocation(response.Headers.Location);
        }

        public async Task<MovieResponse> GetMovieAsync(Guid movieId)
    {
        var response = await client.GetAsync($"/api/movie-catalog/{movieId}");
        response.EnsureSuccessStatusCode();
        return await TestUtils.ReadAsAsync<MovieResponse>(response);
    }
}
public sealed record MovieResponse
{
    public Guid Id { get; init; }
    public MovieTitle Title { get; init; }
    public string Description { get; init; }
    public AgeRating AgeRating { get; init; }
    public ReleaseYear ReleaseYear { get; init; }
    public Duration Duration { get; init; }
    public List<Genre> Genres { get; init; }
    public List<Actor> Actors { get; init; }
    public PosterUrl PosterUrl { get; init; }
}