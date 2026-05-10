using Aornis;
using Howestprime.Movies.Domain.MovieEvent;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;

public sealed class MovieEventRepository(
    DomainDbContext context
    ): EfCoreGenericRepository<MovieEvent, MovieEventId>(context), IMovieEventRepository
{
    

}