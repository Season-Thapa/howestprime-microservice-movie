using Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Repositories;
public sealed class MovieRepository(
    DomainDbContext context
    ) : EfCoreGenericRepository<Movie, MovieId>(context), IMovieRepository
{

}