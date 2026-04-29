using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace UnitTests.Domain.Movies.ValueObjects;

public class PosterUrlTests
{
    [Fact]
    public void Create_ShouldReturnPosterUrl_WhenValueIsValid()
    {
        // Arrange
        var validValue = "https://example.com/poster.jpg";

        // Act
        var result = PosterUrl.Create(validValue);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(validValue, result.Value);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenValueIsEmpty()
    {
        // Arrange
        var invalidValue = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => PosterUrl.Create(invalidValue));
    }
}