using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace UnitTests.Domain.Movies.ValueObjects;

public class ReleaseYearTests
{
    [Fact]
    public void Create_ShouldCreateDuration_WhenReleaseYear()
    {
        // Arrange
        int releaseYearValue = 2002;;

        // Act
        ReleaseYear releaseYear = ReleaseYear.Create(releaseYearValue);

        // Assert
        Assert.Equal(releaseYearValue, releaseYear.Value);
    }
    [Fact]
    public void Create_ShouldThrowArgumentException_WhenReleasesIsNegative()
    {
        // Arrange
        int releaseYearValue = -10;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ReleaseYear.Create(releaseYearValue));
    }

    [Fact]
    public void ReleaseYears_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        var ry1 = ReleaseYear.Create(2002);
        var ry2 = ReleaseYear.Create(2002);

        // Act & Assert
        Assert.Equal(ry1, ry2);
    }
}
