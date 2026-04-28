using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies;
public interface IMovieRepository : IRepository<Movie, MovieId>
{

}
