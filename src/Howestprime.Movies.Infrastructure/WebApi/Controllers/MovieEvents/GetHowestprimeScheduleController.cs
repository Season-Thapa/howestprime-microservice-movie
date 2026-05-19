using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;

public static class GetHowestprimeScheduleController
{
    public static async Task<IResult> Invoke(
        [FromServices] IUseCase<SearchMovieEventsInTimeRangeInput, IReadOnlyList<MovieEventData>> useCase
    )
    {
        var from = DateTime.UtcNow;
        var to = from.AddDays(14);
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
        var response = new HowestprimeSchedule
        {
            MovieIds = movieEventResponse.Select(me => me.Movie.Id).ToList(),
            MovieEvents = movieEventResponse
        };
        return TypedResults.Ok(response);
    }
    
}