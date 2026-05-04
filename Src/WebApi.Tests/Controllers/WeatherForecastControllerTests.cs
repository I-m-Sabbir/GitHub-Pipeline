using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests.Controllers;

public class WeatherForecastControllerTests
{
    private readonly WeatherForecastController _controller;
    private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;

    public WeatherForecastControllerTests()
    {
        _loggerMock = new Mock<ILogger<WeatherForecastController>>();
        _controller = new WeatherForecastController(_loggerMock.Object);
    }

    [Fact]
    public void Get_ReturnsExactlyFiveForecasts()
    {
        // Act
        var result = _controller.Get();

        // Assert
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public void Get_AllTemperatures_AreWithinValidRange()
    {
        // Arrange
        const int minTemp = -20;
        const int maxTemp = 55; // Random.Next upper bound is exclusive, so max is 54

        // Act
        var result = _controller.Get().ToList();

        // Assert
        Assert.All(result, forecast =>
        {
            Assert.True(forecast.TemperatureC >= minTemp,
                $"Temperature {forecast.TemperatureC}°C is below minimum {minTemp}°C");
            Assert.True(forecast.TemperatureC < maxTemp,
                $"Temperature {forecast.TemperatureC}°C is at or above maximum {maxTemp}°C");
        });
    }

    [Fact]
    public void Get_AllSummaries_AreFromPredefinedList()
    {
        // Arrange
        var validSummaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // Act
        var result = _controller.Get().ToList();

        // Assert
        Assert.All(result, forecast =>
        {
            Assert.Contains(forecast.Summary, validSummaries);
        });
    }

    [Fact]
    public void Get_AllDates_AreInTheFuture()
    {
        // Arrange
        var today = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = _controller.Get().ToList();

        // Assert
        Assert.All(result, forecast =>
        {
            Assert.True(forecast.Date > today,
                $"Date {forecast.Date} should be after today ({today})");
        });
    }

    [Fact]
    public void Get_Dates_AreConsecutiveDays()
    {
        // Act
        var result = _controller.Get().ToList();

        // Assert
        for (int i = 1; i < result.Count; i++)
        {
            var previousDate = result[i - 1].Date;
            var currentDate = result[i].Date;
            var expectedDate = previousDate.AddDays(1);

            Assert.Equal(expectedDate, currentDate);
        }
    }

    [Fact]
    public void Get_FirstDate_IsTomorrow()
    {
        // Arrange
        var tomorrow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        // Act
        var result = _controller.Get().First();

        // Assert
        Assert.Equal(tomorrow, result.Date);
    }

    [Fact]
    public void Get_TemperatureFahrenheit_IsCalculatedCorrectly()
    {
        // Act
        var result = _controller.Get().ToList();

        // Assert
        Assert.All(result, forecast =>
        {
            var expectedFahrenheit = 32 + (int)(forecast.TemperatureC / 0.5556);
            Assert.Equal(expectedFahrenheit, forecast.TemperatureF);
        });
    }

    [Fact]
    public void Get_ReturnsNonNullResult()
    {
        // Act
        var result = _controller.Get();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Get_EachForecast_HasNonNullSummary()
    {
        // Act
        var result = _controller.Get().ToList();

        // Assert
        Assert.All(result, forecast =>
        {
            Assert.NotNull(forecast.Summary);
            Assert.NotEmpty(forecast.Summary);
        });
    }
}
