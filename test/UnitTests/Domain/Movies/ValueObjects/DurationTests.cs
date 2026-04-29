using Howestprime.Movies.Domain.Movies.ValueObjects;

namespace UnitTests.Domain.Movies.ValueObjects;

public class DurationTests
{
    [Fact]
    public void Create_ShouldCreateDuration_WhenValidMinutes()
    {
        // Arrange
        int minutes = 120;

        // Act
        Duration duration = Duration.Create(minutes);

        // Assert
        Assert.Equal(minutes, duration.Value);
    }


[Fact]
public void Create_ShouldThrowArgumentException_WhenMinutesIsZero()
{
    // Arrange
    int minutes = 0;

    // Act & Assert
    Assert.Throws<ArgumentException>(() => Duration.Create(minutes));
}

[Fact]
public void Create_ShouldThrowArgumentException_WhenMinutesIsNegative()
{
    // Arrange
    int minutes = -10;

    // Act & Assert
    Assert.Throws<ArgumentException>(() => Duration.Create(minutes));
}

[Fact]
public void Durations_WithSameValue_ShouldBeEqual()
{
    // Arrange
    var d1 = Duration.Create(100);
    var d2 = Duration.Create(100);

    // Act & Assert
    Assert.Equal(d1, d2);
}
}