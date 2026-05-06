﻿using Howestprime.Movies.Application.Movies;
using Howestprime.Movies.Domain.Movies;
using Howestprime.Movies.Domain.Movies.Events;
using Howestprime.Movies.Domain.Movies.ValueObjects;
using UnitTests.Application.Mocks;

namespace UnitTests.Application;

public sealed class RegisterMovieTests
{
    [Fact]
    public async Task Execute_ShouldRegisterMovie_SaveIt_AndReturnMovieId()
    {
        // Arrange
        FakeUnitOfWork fakeUow = new();

        RegisterMovie useCase = new(fakeUow);

        RegisterMovieInput input = CreateValidInput();

        // Act
        Guid result = await useCase.Execute(input);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        Assert.Equal(1, fakeUow.SaveCalls);
        Assert.NotNull(fakeUow.SavedAggregateRoot);
        Movie savedMovie = Assert.IsType<Movie>(fakeUow.SavedAggregateRoot);
        Assert.Equal(input.Title, savedMovie.Title.Value);
        Assert.Equal(input.Description, savedMovie.Description);
        Assert.Equal(input.AgeRating, savedMovie.AgeRating.Value);
        Assert.Equal(input.ReleaseYear, savedMovie.ReleaseYear.Value);
        Assert.Equal(input.Duration, savedMovie.Duration.Value);
        Assert.Equal(input.PosterUrl, savedMovie.PosterUrl.Value);

        Assert.Equal(input.Genres.Count, savedMovie.Genres.Count);
        Assert.Equal(input.Actors.Count, savedMovie.Actors.Count);
        Assert.Equal(savedMovie.Id.Value, result);
    }
    private static RegisterMovieInput CreateValidInput()
    {
        return new RegisterMovieInput(
            "Inception",
            "A mind-bending thriller about dreams within dreams.",
            2010,
            13,
            148,
            new List<string>
            {
               "Action",
                "Sci-Fi",
                    "Thriller"
            },
            new List<string>
            {
                "Leonardo DiCapri",
                "Joseph Gordon-Levitt",
                "Elliot Page"
            },
            "https://example.com/inception-poster.jpg"
        );
    }
    [Fact]
    public async Task Execute_ShouldRaiseMovieRegisteredDomainEvent()
    {
        // Arrange
        FakeUnitOfWork fakeUow = new();
        RegisterMovie useCase = new(fakeUow);

        RegisterMovieInput input = CreateValidInput();

        // Act
        await useCase.Execute(input);

        // Assert
        Movie savedMovie = Assert.IsType<Movie>(fakeUow.SavedAggregateRoot);

        Assert.Contains(
            savedMovie.DomainEvents,
            e => e is MovieRegistered
        );
    }
}