using System.Linq.Expressions;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;

namespace UnitTests.Application.Movies;

public class SearchMovieCatalogTests
{
    [Fact]
    public async Task Execute_WithExistingMovie_ShouldReturnMovie()
    {
        
        MovieData data = new()  
        {
            Id = Guid.NewGuid(),
            Title = "Test Movie",
            Description = "Test Description",
            AgeRating = 12,
            ReleaseYear = 2020,
            Duration = 120,
            Actors = new List<ActorData> { new ActorData { Value = "Actor 1" }, new ActorData { Value = "Actor 2" } },
            Genres = new List<GenreData> { new GenreData { Value = "Action" } },
            PosterUrl = "http://example.com/poster.jpg"
            
        };

        ISearchMovieCatalogQuery query = new FakeSearchMovieCatalogQuery(new List<MovieData> { data });
        SearchMovieCatalog usecase = new(query);
        IReadOnlyList<GenreData> genres = new List<GenreData>
        {
            new GenreData { Value = "Action" }
        };

        IReadOnlyList<MovieData> result = await usecase.Execute(new SearchMovieCatalogInput(data.Title, genres));
        //Assert
        Assert.True(result.Count == 1);
        Assert.True(result[0].Id == data.Id);
        
        
    }
    private sealed class FakeSearchMovieCatalogQuery(IReadOnlyList<MovieData> result) : ISearchMovieCatalogQuery
    {
        private readonly IReadOnlyList<MovieData> _result = result;

        public Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> movieFilter)
        {
            return Task.FromResult(_result);
        }
    }
}