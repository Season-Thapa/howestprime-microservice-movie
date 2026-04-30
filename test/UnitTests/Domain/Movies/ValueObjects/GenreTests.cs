using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace UnitTests.Domain.Movies.ValueObjects;

public class GenreTests
{
    [Fact]
    public void Create_ShouldReturnGenre_WhenValidValue()
    {
        // Arrange
        List<Genre> genres = new()
        {
            Genre.Create("Action"),
            Genre.Create("Sci-Fi"),
            Genre.Create("Thriller")
        };

        // Act
        Genre result = genres[0];
        // Assert
        Assert.Equal("Action", result.Value);
    }
    [Fact]
    public void Create_ShouldThrowException_WhenValueIsEmpty()
    {
        // Arrange
        var value = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Genre.Create(value));
    }
}