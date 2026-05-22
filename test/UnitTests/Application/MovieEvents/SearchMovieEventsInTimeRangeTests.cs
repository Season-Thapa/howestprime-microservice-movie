using System.Linq.Expressions;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Moq;
using UnitTests.Application.Mocks;

namespace UnitTests.Application.MovieEvents;

public class SearchMovieEventsInTimeRangeTests
{
	[Fact]
	public async Task Execute_WithEventsInAndOutOfRange_ShouldReturnOnlyInRange()
	{
		DateTime now = DateTime.UtcNow;
		MovieEventData inRange = new()
		{
			Id = Guid.NewGuid(),
			Showtime = now.AddDays(7),
			Capacity = 100,
			Movie = new MovieData
			{
				Id = Guid.NewGuid(),
				Title = "InRange",
				Description = "Desc",
				ReleaseYear = 2024,
				Genres = new List<GenreData>(),
				Actors = new List<ActorData>(),
				AgeRating = 12,
				Duration = 100,
				PosterUrl = "http://example.com/poster.jpg"
			},
			Room = new RoomData { Id = Guid.NewGuid(), Name = "Room 1", Capacity = 100 }
		};

		MovieEventData outOfRange = new()
		{
			Id = Guid.NewGuid(),
			Showtime = now.AddDays(15),
			Capacity = 80,
			Movie = new MovieData
			{
				Id = Guid.NewGuid(),
				Title = "OutOfRange",
				Description = "Desc",
				ReleaseYear = 2024,
				Genres = new List<GenreData>(),
				Actors = new List<ActorData>(),
				AgeRating = 12,
				Duration = 100,
				PosterUrl = "http://example.com/poster.jpg"
			},
			Room = new RoomData { Id = Guid.NewGuid(), Name = "Room 2", Capacity = 80 }
		};

		IMovieEventQuery query = new FakeMovieEventQuery(new List<MovieEventData> { inRange, outOfRange });
		SearchMovieEventsInTimeRange usecase = new(query);

		DateTime from = now;
		DateTime to = now.AddDays(14);

		IReadOnlyList<MovieEventData> result = await usecase.Execute(new SearchMovieEventsInTimeRangeInput(from, to));

		Assert.Single(result);
		Assert.Equal(inRange.Id, result[0].Id);
	}

	[Fact]
	public async Task Execute_WithNoEventsInRange_ShouldReturnEmptyList()
	{
		DateTime now = DateTime.UtcNow;

		MovieEventData outOfRange = new()
		{
			Id = Guid.NewGuid(),
			Showtime = now.AddDays(20),
			Capacity = 80,
			Movie = new MovieData
			{
				Id = Guid.NewGuid(),
				Title = "OutOfRange",
				Description = "Desc",
				ReleaseYear = 2024,
				Genres = new List<GenreData>(),
				Actors = new List<ActorData>(),
				AgeRating = 12,
				Duration = 100,
				PosterUrl = "http://example.com/poster.jpg"
			},
			Room = new RoomData { Id = Guid.NewGuid(), Name = "Room 2", Capacity = 80 }
		};

		IMovieEventQuery query = new FakeMovieEventQuery(new List<MovieEventData> { outOfRange });
		SearchMovieEventsInTimeRange usecase = new(query);

		DateTime from = now;
		DateTime to = now.AddDays(14);

		IReadOnlyList<MovieEventData> result = await usecase.Execute(new SearchMovieEventsInTimeRangeInput(from, to));

		Assert.Empty(result);
	}

	
}