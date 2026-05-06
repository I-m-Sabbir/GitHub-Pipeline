using Bunit;
using BlazorApp.Components.Pages;
using Xunit;

namespace BlazorApp.Tests.Components.Pages;

public class WeatherTests : TestContext
{
    [Fact]
    public void Weather_InitialState_ShowsLoadingIndicator()
    {
        // Act
        var cut = RenderComponent<Weather>();

        // Assert - Before async operation completes, should show loading
        var loadingText = cut.Find("em");
        Assert.Equal("Loading...", loadingText.TextContent);
    }

    [Fact]
    public async Task Weather_AfterLoading_DisplaysTable()
    {
        // Arrange
        var cut = RenderComponent<Weather>();

        // Act - Wait for async initialization to complete
        await Task.Delay(600); // Wait longer than the 500ms delay in the component
        cut.Render(); // Re-render to reflect state changes

        // Assert
        var table = cut.Find("table");
        Assert.NotNull(table);
        Assert.Contains("table", table.ClassName);
    }

    [Fact]
    public async Task Weather_AfterLoading_DisplaysFiveForecasts()
    {
        // Arrange
        var cut = RenderComponent<Weather>();

        // Act
        await Task.Delay(600);
        cut.Render();

        // Assert
        var rows = cut.FindAll("tbody tr");
        Assert.Equal(5, rows.Count);
    }

    [Fact]
    public async Task Weather_Table_HasCorrectHeaders()
    {
        // Arrange
        var cut = RenderComponent<Weather>();

        // Act
        await Task.Delay(600);
        cut.Render();

        // Assert
        var headers = cut.FindAll("thead th");
        Assert.Equal(4, headers.Count);
        Assert.Equal("Date", headers[0].TextContent);
        Assert.Equal("Temp. (C)", headers[1].TextContent);
        Assert.Equal("Temp. (F)", headers[2].TextContent);
        Assert.Equal("Summary", headers[3].TextContent);
    }

    [Fact]
    public async Task Weather_ForecastRows_HaveFourColumns()
    {
        // Arrange
        var cut = RenderComponent<Weather>();

        // Act
        await Task.Delay(600);
        cut.Render();

        // Assert
        var firstRow = cut.Find("tbody tr");
        var cells = firstRow.QuerySelectorAll("td");
        Assert.Equal(4, cells.Length);
    }

    [Fact]
    public async Task Weather_Temperatures_AreWithinValidRange()
    {
        // Arrange
        var cut = RenderComponent<Weather>();

        // Act
        await Task.Delay(600);
        cut.Render();

        // Assert - Check all temperature cells (second column, index 1)
        var rows = cut.FindAll("tbody tr");
        foreach (var row in rows)
        {
            var tempCell = row.QuerySelectorAll("td")[1];
            var tempC = int.Parse(tempCell.TextContent);
            Assert.InRange(tempC, -20, 54); // Random.Next(-20, 55) generates -20 to 54
        }
    }

    [Fact]
    public async Task Weather_Summaries_AreFromPredefinedList()
    {
        // Arrange
        var validSummaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        var cut = RenderComponent<Weather>();

        // Act
        await Task.Delay(600);
        cut.Render();

        // Assert - Check all summary cells (fourth column, index 3)
        var rows = cut.FindAll("tbody tr");
        foreach (var row in rows)
        {
            var summaryCell = row.QuerySelectorAll("td")[3];
            Assert.Contains(summaryCell.TextContent, validSummaries);
        }
    }

    [Fact]
    public void Weather_Heading_DisplaysCorrectText()
    {
        // Act
        var cut = RenderComponent<Weather>();

        // Assert
        var heading = cut.Find("h1");
        Assert.Equal("Weather", heading.TextContent);
    }

    [Fact]
    public void Weather_Description_IsPresent()
    {
        // Act
        var cut = RenderComponent<Weather>();

        // Assert
        var description = cut.Find("p");
        Assert.Equal("This component demonstrates showing data.", description.TextContent);
    }
}
