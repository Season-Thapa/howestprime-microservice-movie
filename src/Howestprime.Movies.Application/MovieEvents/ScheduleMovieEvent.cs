using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Domain.Movies;

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


        MovieEvent movieEvent = MovieEvent.Schedule(input.MovieId, input.RoomId, input.ShowTime, input.Capacity,movieEventId);
        
        await unitOfWork.Save<IMovieEventRepository>(movieEvent);
        await unitOfWork.Do();

        return movieEvent.Id.Value;
    }
}