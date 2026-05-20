using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;

public static class GetMovieEventByMonthController
{
    public static async Task<IResult> Invoke(
        [FromServices] IUseCase<SearchMovieEventsInTimeRangeInput, IReadOnlyList<MovieEventData>> useCase,
        [FromQuery] int year,
        [FromQuery] int month
    )
    {
        DateTime from = new(year, month, 1);

        DateTime to = from.AddMonths(1).AddTicks(-1);

        var input = new SearchMovieEventsInTimeRangeInput(from, to);

        IReadOnlyList<MovieEventData> movieEvents;
        try
        {
            movieEvents = await useCase.Execute(input);
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
        }
        var movieEventResponse = movieEvents.Select(me => new MovieEventData
        {
            Id = me.Id,
            Room = me.Room,
            Movie = me.Movie,
            ShowTime = me.ShowTime,
            Capacity = me.Capacity,

        }).ToList();

        return Results.Ok(movieEventResponse);
    }
    
    
}