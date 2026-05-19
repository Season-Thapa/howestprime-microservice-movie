using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.MovieEvents;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents
{
    public record ScheduleMovieEventRequest(
    [FromBody] ScheduleMovieEventBody Body,
    [ScheduleMovieEventRequest.FromService] IUseCase<ScheduleMovieEventInput, Guid> UseCase)
    {
        public class FromServiceAttribute : Attribute
        {
        }
    }

    public static class ScheduleMovieEventController
    {
       
    }
    public record ScheduleMovieEventBody(
    [Required] Guid MovieId,
    [Required] Guid RoomId,
    [Required] DateTime ShowTime,
    [Required] int Capacity
);
}
