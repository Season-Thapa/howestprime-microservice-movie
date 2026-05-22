using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.ValueObjects;
using Howestprime.Movies.Domain.Rooms;
using Howestprime.Movies.Shared.Exceptions;
using UnitTests.Application.Mocks;

namespace UnitTests.Application.MovieEvents;

public sealed class ScheduleMovieEventTests
{
    [Fact]
    public async Task Execute_ShouldScheduleMovieEvent_WhenInputIsValid()
    {
        // Arrange
        FakeUnitOfWork unitOfWork = new();

        FakeMovieRepository movieRepository = new();
        FakeRoomRepository roomRepository = new();
        FakeMovieEventRepository movieEventRepository = new();

        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();

        await movieRepository.Save(movie);
        await roomRepository.Save(room);

        unitOfWork.SetRepository<IMovieRepository>(movieRepository);
        unitOfWork.SetRepository<IRoomRepository>(roomRepository);
        unitOfWork.SetRepository<IMovieEventRepository>(movieEventRepository);

        ScheduleMovieEvent useCase = new(unitOfWork);
        DateTime showTime = DateTime.Today
            .AddDays(1)
            .AddHours(15);

        ScheduleMovieEventInput input = new(
            movie.Id.Value,
            room.Id.Value,      
            showTime

        );

        // Act
        Guid result = await useCase.Execute(input);

        // Assert
        Assert.NotEqual(Guid.Empty, result);

        Assert.Equal(1, unitOfWork.SaveCalls);
        Assert.Equal(1, unitOfWork.DoCalls);

        Assert.NotNull(unitOfWork.SavedAggregateRoot);
        Assert.IsType<MovieEvent>(unitOfWork.SavedAggregateRoot);
    }

    [Fact]
    public async Task Execute_ShouldThrowNotFoundException_WhenMovieDoesNotExist()
    {
        // Arrange
        FakeUnitOfWork unitOfWork = new();

        FakeMovieRepository movieRepository = new();
        FakeRoomRepository roomRepository = new();
        FakeMovieEventRepository movieEventRepository = new();

        Room room = CreateValidRoom();
        await roomRepository.Save(room);

        unitOfWork.SetRepository<IMovieRepository>(movieRepository);
        unitOfWork.SetRepository<IRoomRepository>(roomRepository);
        unitOfWork.SetRepository<IMovieEventRepository>(movieEventRepository);

        ScheduleMovieEvent useCase = new(unitOfWork);

        ScheduleMovieEventInput input = new(
            Guid.NewGuid(),
            room.Id.Value,
            DateTime.UtcNow.AddDays(1)
        );

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => useCase.Execute(input)
        );
    }

    [Fact]
    public async Task Execute_ShouldThrowNotFoundException_WhenRoomDoesNotExist()
    {
        // Arrange
        FakeUnitOfWork unitOfWork = new();

        FakeMovieRepository movieRepository = new();
        FakeRoomRepository roomRepository = new();
        FakeMovieEventRepository movieEventRepository = new();

        Movie movie = CreateValidMovie();
        await movieRepository.Save(movie);

        unitOfWork.SetRepository<IMovieRepository>(movieRepository);
        unitOfWork.SetRepository<IRoomRepository>(roomRepository);
        unitOfWork.SetRepository<IMovieEventRepository>(movieEventRepository);

        ScheduleMovieEvent useCase = new(unitOfWork);

        ScheduleMovieEventInput input = new(
            movie.Id.Value,
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(1)
        );

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => useCase.Execute(input)
        );
    }

    [Fact]
    public async Task Execute_ShouldThrowInvalidOperationException_WhenMovieEventAlreadyExists()
    {
        // Arrange
        FakeUnitOfWork unitOfWork = new();

        FakeMovieRepository movieRepository = new();
        FakeRoomRepository roomRepository = new();
        FakeMovieEventRepository movieEventRepository = new();

        Movie movie = CreateValidMovie();
        Room room = CreateValidRoom();

        await movieRepository.Save(movie);
        await roomRepository.Save(room);

        DateTime showTime = DateTime.Today.AddDays(1).AddHours(15).AddMinutes(00);

        MovieEvent existingMovieEvent = MovieEvent.Schedule(
            movie,
            room,
            showTime,
            room.Capacity
        );

        await movieEventRepository.Save(existingMovieEvent);

        unitOfWork.SetRepository<IMovieRepository>(movieRepository);
        unitOfWork.SetRepository<IRoomRepository>(roomRepository);
        unitOfWork.SetRepository<IMovieEventRepository>(movieEventRepository);

        ScheduleMovieEvent useCase = new(unitOfWork);

        ScheduleMovieEventInput input = new(
            movie.Id.Value,
            room.Id.Value,
            showTime
        );

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => useCase.Execute(input)
        );
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