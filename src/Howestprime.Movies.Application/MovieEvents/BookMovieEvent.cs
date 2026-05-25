using Aornis;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Domain.MovieEvents;
using Howestprime.Movies.Domain.MovieEvents.Entities;
using Howestprime.Movies.Domain.Rooms;
using Howestprime.Movies.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace Howestprime.Movies.Application.MovieEvents;
public sealed record BookMovieEventInput(
    Guid MovieEventId, 
    int StandardVisitors,
    int DiscountVisitors);
public sealed class BookMovieEvent(
    IUnitOfWork unitOfWork,
    ILogger<BookMovieEvent> logger
    ) : IUseCase<BookMovieEventInput, BookingId>
{
    public async Task<BookingId> Execute(BookMovieEventInput input)
    {
        MovieEventId movieEventId = new(input.MovieEventId);
         MovieEvent? movieEvent = (await unitOfWork.Repo<IMovieEventRepository>().ById(movieEventId))
             .OrElseThrow(() => new NotFoundException($"MovieEvent with ID {movieEventId} does not exist."));
         Optional<Room> roomOptional = await unitOfWork.Repo<IRoomRepository>().ById(movieEvent.RoomId);

         int totalVisitors = input.StandardVisitors + input.DiscountVisitors;
         if (totalVisitors >= movieEvent.Capacity)
         {
             throw new InvalidOperationException($"Not enough capacity for {totalVisitors} visitors. Remaining capacity: {movieEvent.Capacity - movieEvent.Visitor}");
         }
         if(totalVisitors <= 0)
         {
             throw new InvalidOperationException($"At least one visitor must be booked. Provided visitors: {totalVisitors}");
         }
         Booking booking = movieEvent.Book(
            input.StandardVisitors,
            input.DiscountVisitors,
            roomOptional.Value.Name
            );
         await unitOfWork.Save<IMovieEventRepository>(movieEvent);
         await unitOfWork.Do();
         logger.LogInformation("Booked movie event with ID {MovieEventId} and created booking with ID {BookingId}", input.MovieEventId, booking.Id.Value);
         return booking.Id;
    }
    
}