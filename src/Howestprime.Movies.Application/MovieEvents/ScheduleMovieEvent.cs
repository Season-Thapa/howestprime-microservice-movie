using Aornis;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using Howestprime.Movies.Shared.Exceptions;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record ScheduleMovieEventInput(
    Guid MovieEventId,
    MovieId MovieId,
    RoomId RoomId,
    DateTime ShowTime,
    int Capacity
);
public class ScheduleMovieEvent(
    IUnitOfWork unitOfWork
    ) : IUseCase<ScheduleMovieEventInput, Guid>
{
    public async Task<Guid> Execute(ScheduleMovieEventInput input)
    {
        MovieEventId movieEventId = new(input.MovieEventId);
        MovieEvent? existedMovieEvent= (await unitOfWork.Repo<IMovieEventRepository>().ByShowtimeAndRoomID(input.ShowTime, input.RoomId));
        Optional<Movie> movieOptional = await unitOfWork.Repo<IMovieRepository>().ById(input.MovieId);

        if (!movieOptional.HasValue)
            throw new NotFoundException($"Movie with ID {input.MovieId} does not exist.");
        if (existedMovieEvent != null)
        {
            throw new InvalidOperationException($"There is already a movie event scheduled at {input.ShowTime} in room {input.RoomId.Value}");
        }

        MovieEvent movieEvent = MovieEvent.Schedule(input.MovieId, input.RoomId, input.ShowTime, input.Capacity,movieEventId);
        
        await unitOfWork.Save<IMovieEventRepository>(movieEvent);
        await unitOfWork.Do();

        return movieEvent.Id.Value;
    }
}