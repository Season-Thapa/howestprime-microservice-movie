using Aornis;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Rooms;
using Howestprime.Movies.Shared.Exceptions;

namespace Howestprime.Movies.Application.MovieEvents;

public sealed record ScheduleMovieEventInput(
    Guid MovieId,
    Guid RoomId,
    DateTime ShowTime
);
public class ScheduleMovieEvent(
    IUnitOfWork unitOfWork
    ) : IUseCase<ScheduleMovieEventInput, Guid>
{
    public async Task<Guid> Execute(ScheduleMovieEventInput input)
    {
        MovieId movieId = new MovieId(input.MovieId);
        RoomId roomId = new RoomId(input.RoomId);
        MovieEvent? existedMovieEvent= (await unitOfWork.Repo<IMovieEventRepository>().ByShowtimeAndRoomID(input.ShowTime, roomId));
        Optional<Movie> movieOptional = await unitOfWork.Repo<IMovieRepository>().ById(movieId);
        Optional<Room> roomOptional = await unitOfWork.Repo<IRoomRepository>().ById(roomId);

        if (!movieOptional.HasValue)
            throw new NotFoundException($"Movie with ID {input.MovieId} does not exist.");
        if (!roomOptional.HasValue)
            throw new NotFoundException($"Room with ID {input.RoomId} does not exist.");
        if (existedMovieEvent != null)
        {
            throw new InvalidOperationException($"There is already a movie event scheduled at {input.ShowTime} in room {roomId.Value}");
        }
        

        MovieEvent movieEvent = MovieEvent.Schedule(movieId, roomId, input.ShowTime, roomOptional.Value.Capacity);
        
        await unitOfWork.Save<IMovieEventRepository>(movieEvent);
        await unitOfWork.Do();

        return movieEvent.Id.Value;
    }
}