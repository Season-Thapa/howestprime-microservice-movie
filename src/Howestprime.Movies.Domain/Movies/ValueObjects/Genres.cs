using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;

public sealed record Genres(string Name) : ValueObject
{
    public sealed class Genres : ValueObject
    {
        public IReadOnlyList<Genres> Value { get; }

        private Genres(IEnumerable<Genres> genres)
        {
            Value = genres.ToList().AsReadOnly();
        }

        public static Genres Create(IEnumerable<Genres> genres)
        {
            var genreList = genres.ToList();
            Asserts.EnsureGreaterThan(genreList.Count, 0);
            return new Genres(genreList);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var genre in Value)
                yield return genre;
        }
    }
}
