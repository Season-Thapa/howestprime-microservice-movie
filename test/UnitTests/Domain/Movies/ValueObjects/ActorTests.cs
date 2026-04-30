using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace UnitTests.Domain.Movies.ValueObjects;

public class ActorTests
{
    [Fact]
    public void Create_ShouldReturnActor_WhenValidValue()
    {
        // Arrange
        List<Actor> actors = new()
        {
            Actor.Create("Leonardo DiCaprio"),
            Actor.Create("Joseph Gordon-Levitt"),
            Actor.Create("Elliot Page")
        };
        // Act
        Actor result = actors[0];
        // Assert
        Assert.Equal("Leonardo DiCaprio", result.Value);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenValueIsEmpty()
    {
        // Arrange
        var value = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Actor.Create(value));
    }
}