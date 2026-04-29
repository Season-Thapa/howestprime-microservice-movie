using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace UnitTests.Domain.Movies.ValueObjects;

public class MovieTitleTests
{
    [Fact]
    public void Create_ShouldReturnMovieTitle_WhenValueIsValid()
    {
        // Arrange
        var value = "Inception";

        // Act
        var result = MovieTitle.Create(value);

        // Assert
        Assert.Equal(value, result.Value);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenValueIsEmpty()
    {
        // Arrange
        var value = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => MovieTitle.Create(value));
    }
}