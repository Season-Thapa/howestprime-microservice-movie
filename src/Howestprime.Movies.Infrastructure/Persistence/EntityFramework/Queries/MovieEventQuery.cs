using System.Linq.Expressions;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries;

public sealed class MovieEventQuery : IMovieEventQuery
{
    private readonly QueryDbContext _context;
    public MovieEventQuery(QueryDbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<MovieEventData>> Fetch(Expression<Func<MovieEventData, bool>> movieEventFilter)
    {
        var movieEvents = await _context.MovieEvents
            .Include(me => me.Movie)
            .Include(me => me.Room)
            .Select(me => new MovieEventData
            {
                Id = me.Id,
                Movie = new MovieData
                {
                    Id = me.Movie.Id,
                    Title = me.Movie.Title,
                    Description = me.Movie.Description,
                    ReleaseYear = me.Movie.ReleaseYear,
                    Genres = me.Movie.Genres,
                    Actors = me.Movie.Actors,
                    AgeRating = me.Movie.AgeRating,
                    PosterUrl = me.Movie.PosterUrl,
                    Duration = me.Movie.Duration
                },
                Room = new RoomData
                {
                    Id = me.Room.Id,
                    Name = me.Room.Name,
                    Capacity =  me.Room.Capacity,
                    
                },
                Showtime = DateTime.SpecifyKind(me.Showtime, DateTimeKind.Utc),
                Capacity = me.Capacity
            })
            .ToListAsync();

        return movieEvents
            .AsQueryable()
            .Where(movieEventFilter.Compile())
            .ToList();
    }
}