namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;

public sealed record SearchMovieCollection
{
	public IReadOnlyList<SearchMovie> Data { get; init; } = [];
}

