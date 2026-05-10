using System.Net.Http.Json;
using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace IntegrationTest.Shared;

public sealed class MovieApiClient(HttpClient client)
{
        public async Task<Guid> RegisterMovieAsync(string title,string description,int ageRating,int releaseYear,int duration,List<string> genres,List<string> actors,string posterUrl)
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
        
        var wrapper = await TestUtils.ReadAsAsync<SearchMovieCollection>(response);
        if (wrapper.Data is null || wrapper.Data.Count == 0)
            throw new InvalidOperationException("Response did not contain any movie data.");

        return wrapper.Data[0];
    }
}
public sealed record MovieResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public int AgeRating { get; init; }
    public int ReleaseYear { get; init; }
    public int Duration { get; init; }
    public List<string> Genres { get; init; }
    public List<string> Actors { get; init; }
    public string PosterUrl { get; init; }
}

public sealed record SearchMovieCollection
{
    public List<MovieResponse> Data { get; init; } = new();
}
