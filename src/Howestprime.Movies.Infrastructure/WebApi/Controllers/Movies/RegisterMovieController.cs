using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Domain.Movies.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;

public record RegisterMovieRequest(
    [FromBody] RegisterMovieBody Body,
    [RegisterMovieRequest.FromService] IUseCase<RegisterMovieInput, Guid> UseCase)
{
    public class FromServiceAttribute : Attribute
    {
    }
}

public static class RegisterMovieController
{
    public static async Task<Results<Created, BadRequest>> Invoke(
        [AsParameters]RegisterMovieRequest request)
    {
        RegisterMovieInput input = new RegisterMovieInput(
            request.Body.Title,
            request.Body.Description,
            request.Body.ReleaseYear,
            request.Body.Duration,
            request.Body.AgeRating,
            request.Body.Genres,
            request.Body.Actors,
            request.Body.PosterUrl);
        Guid movieId = await request.UseCase.Execute(input);
        return TypedResults.Created($"/api/movie-catalog/{movieId}");
    }
}
public record RegisterMovieBody(
    [Required] MovieTitle Title,
    [Required] string Description,
    [Required] ReleaseYear ReleaseYear,
    [Required] List<Genre> Genres,
    [Required] List<Actor> Actors,
    [Required] Duration Duration,
    [Required] AgeRating AgeRating,
    [Required] PosterUrl PosterUrl
);
