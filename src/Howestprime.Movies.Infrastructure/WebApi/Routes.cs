using Howestprime.Movies.Infrastructure.WebApi.Controllers.MovieEvents;
using Howestprime.Movies.Infrastructure.WebApi.Controllers.Movies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Howestprime.Movies.Infrastructure.WebApi;

public static class Routes
{
    public static IEndpointRouteBuilder MapRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder webApi = app.MapGroup("/api");
        webApi.MapMoviesRoutes();
        webApi.MapMovieEventsRoutes();
        return app;
    }
    private static RouteGroupBuilder MapMoviesRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder movies = app.MapGroup("/movie-catalog")
        .WithTags("Movies")
        .WithDescription("All endpoints related to managing the movie catalog.");

        movies.MapPost("/", RegisterMovieController.Invoke)
            .WithName("RegisterMovie")
            .WithDescription("Register a new movie in the catalog.");

        movies.MapGet("/", SearchMovieCatalogController.Invoke)
            .WithName("SearchMovieCatalog")
            .WithDescription("Search the movie catalog by title and/or genres.");
        
        movies.MapGet("/{movieId}", FindMovieByIdController.Invoke)
            .WithName("GetMovie")
            .WithDescription("Get a movie by its ID.");
        


        return movies;

    }
    private static RouteGroupBuilder MapMovieEventsRoutes(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder movieEvents = app.MapGroup("/movie-events")
            .WithTags("MovieEvents")
            .WithDescription("All endpoints related to managing movie events.");

        movieEvents.MapPost("/", ScheduleMovieEventController.Invoke)
            .WithName("ScheduleMovieEvent")
            .WithDescription("Schedule a new movie event.");
        movieEvents.MapGet("/", GetMovieEventByMonthController.Invoke)
            .WithName("GetMovieEventsByMonth")
            .WithDescription("Get the movie events by given month and year.");
        movieEvents.MapGet("/howestprime-schedule", GetHowestprimeScheduleController.Invoke)
            .WithName("SearchMovieEvents")
            .WithDescription("Search movie events by date range.");
        movieEvents.MapPost("/{movieEventId}/bookings", BookMovieEventController.Invoke)
            .WithName("GetMovieEvent")
            .WithDescription("Get a movie event by its ID.");
        return movieEvents;
    }

    
}