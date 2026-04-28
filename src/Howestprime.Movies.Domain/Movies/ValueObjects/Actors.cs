using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies.ValueObjects;
{
    public sealed class Actors : ValueObject
    {
        public IReadOnlyList<Actor> Value { get; }

        private Actors(IEnumerable<Actor> actors)
        {
            Value = actors.ToList().AsReadOnly();
        }

        public static Actors Create(IEnumerable<Actor> actors)
        {
            var actorList = actors.ToList();
            Asserts.Ensure(actorList.Count > 0, "Actors list cannot be empty.");
             Asserts.EnsureGreaterThan(actorList.Count, 0);
             return new Actors(actorList);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var actor in Value)
                yield return actor;
        }
    }
}
