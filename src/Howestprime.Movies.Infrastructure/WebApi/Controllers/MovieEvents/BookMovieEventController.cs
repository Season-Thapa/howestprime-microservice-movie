using System.ComponentModel.DataAnnotations;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Domain.MovieEvents.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;
public record BookMovieEventRequest
(
    [FromRoute] Guid MovieEventId,
    [FromBody] BookMovieEventBody Body,
    [BookMovieEventRequest.FromService] IUseCase<BookMovieEventInput, BookingId> UseCase)
{
    public class FromServiceAttribute : Attribute
    {
    }
}

public static class BookMovieEventController
{
    public static async Task<Results<Created, BadRequest>> Invoke([AsParameters] BookMovieEventRequest request)
    {
        BookMovieEventInput input = new(
            request.MovieEventId,
            request.Body.StandardVisitors ?? 0,
            request.Body.DiscountedVisitors ?? 0
        );

        BookingId bookingId = await request.UseCase.Execute(input);

        return TypedResults.Created($"/movie-events/{request.MovieEventId}/bookings/{bookingId}");
    }
    
}

public record BookMovieEventBody
(
    int? StandardVisitors,
    int? DiscountedVisitors
);