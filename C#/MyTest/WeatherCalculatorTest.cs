using MyLibrary;

namespace MyTest;

public class WeatherCalculatorTest
{
    [Theory]
    [InlineData(3, "Spring")]
    [InlineData(4, "Spring")]
    [InlineData(5, "Spring")]
    [InlineData(6, "Summer")]
    [InlineData(7, "Summer")]
    [InlineData(8, "Summer")]
    [InlineData(9, "Fall")]
    [InlineData(10, "Fall")]
    [InlineData(11, "Fall")]
    [InlineData(12, "Winter")]
    [InlineData(1, "Winter")]
    [InlineData(2, "Winter")]
    public void DetermineSeason_ReturnsCorrectSeason(int month, string expectedSeason)
    {
        var date = new DateOnly(2024, month, 1);
        var result = WeatherCalculator.DetermineSeason(date);
        Assert.Equal(expectedSeason, result);
    }
}
