using Howestprime.Movies.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Howestprime.Movies.Domain.Movies;
public readonly record struct MovieId(Guid Value) : IEntityId;

public sealed class Movie : AggregateRoot<MovieId>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int AgeRating { get; private set; }
    public int ReleaseYear { get; private set; }
    public int Duration { get; private set; }
    public List<string> Genres { get; private set; }
    public List<Actor> Actors { get; private set; }
    public string PosterUrl { get; private set; }
    public void Register()
    {

    }

}
