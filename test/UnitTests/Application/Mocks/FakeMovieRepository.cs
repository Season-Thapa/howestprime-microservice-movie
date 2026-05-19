using Aornis;
using Howestprime.Movies.Domain.Movies;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Application.Mocks
{
    public sealed class FakeMovieRepository : IMovieRepository
    {
        private readonly HashSet<Movie> _movies = [];
        public Task<Optional<Movie>> ById(MovieId id)
        {
            return Task.FromResult(
           Optional.Of(_movies.FirstOrDefault(m => m.Id.Equals(id)))
       );
        }

        public Task<bool> Exists(MovieId id)
        {
            return Task.FromResult(
                _movies.Any(movie => movie.Id.Equals(id))
            );
        }

        public Task Remove(Movie aggregateRoot)
        {
            return Task.FromResult(
                _movies.Remove(aggregateRoot)
            );
        }

        public Task Save(Movie aggregateRoot)
        {
            _movies.Add(aggregateRoot);
            return Task.CompletedTask;
        }
    }
}
