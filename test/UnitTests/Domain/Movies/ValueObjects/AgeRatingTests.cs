using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace UnitTests.Domain.Movies.ValueObjects;

public class AgeRatingTests
{
    [Fact]
    public void Create_ShouldReturnAgeRating_WhenValueIsValid()
    {
        // Arrange
        int validValue = 18;

        // Act
        var ageRating = AgeRating.Create(validValue);

        // Assert
        Assert.Equal(validValue, ageRating.Value);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenValueIsInvalid()
    {
        // Arrange
        int invalidValue = -1;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => AgeRating.Create(invalidValue));
    }
}