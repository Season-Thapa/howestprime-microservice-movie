using System.Linq.Expressions;
using Howestprime.Movies.Application.Contracts.Data;
using Howestprime.Movies.Application.Contracts.Ports;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Queries;

public sealed class SearchMovieCatalogQuery : ISearchMovieCatalogQuery
{
    private readonly QueryDbContext _context;

    public SearchMovieCatalogQuery(QueryDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<MovieData>> Fetch(Expression<Func<MovieData, bool>> movieFilter)
    {
        var movies = await _context.Movies.ToListAsync();

        return movies
            .AsQueryable()
            .Where(movieFilter.Compile())
            .ToList();
    }
}