using Bunit;
using BlazorApp.Components.Pages;
using Xunit;

namespace BlazorApp.Tests.Components.Pages;

public class CounterTests : TestContext
{
    [Fact]
    public void Counter_InitialState_DisplaysZero()
    {
        // Act
        var cut = RenderComponent<Counter>();

        // Assert
        var paragraph = cut.Find("p[role='status']");
        Assert.Equal("Current count: 0", paragraph.TextContent);
    }

    [Fact]
    public void Counter_WhenButtonClicked_IncrementsCount()
    {
        // Arrange
        var cut = RenderComponent<Counter>();

        // Act
        cut.Find("button").Click();

        // Assert
        var paragraph = cut.Find("p[role='status']");
        Assert.Equal("Current count: 1", paragraph.TextContent);
    }

    [Fact]
    public void Counter_WhenButtonClickedMultipleTimes_IncrementsCorrectly()
    {
        // Arrange
        var cut = RenderComponent<Counter>();
        var button = cut.Find("button");

        // Act
        button.Click();
        button.Click();
        button.Click();

        // Assert
        var paragraph = cut.Find("p[role='status']");
        Assert.Equal("Current count: 3", paragraph.TextContent);
    }

    [Fact]
    public void Counter_Button_HasCorrectText()
    {
        // Act
        var cut = RenderComponent<Counter>();

        // Assert
        var button = cut.Find("button");
        Assert.Equal("Click me", button.TextContent);
    }

    [Fact]
    public void Counter_Button_HasPrimaryBootstrapClass()
    {
        // Act
        var cut = RenderComponent<Counter>();

        // Assert
        var button = cut.Find("button");
        Assert.Contains("btn-primary", button.ClassName);
    }

    [Fact]
    public void Counter_HasPageTitleComponent()
    {
        // Act
        var cut = RenderComponent<Counter>();

        // Assert - Verify PageTitle component is rendered
        var hasPageTitle = cut.HasComponent<Microsoft.AspNetCore.Components.Web.PageTitle>();
        Assert.True(hasPageTitle);
    }

    [Fact]
    public void Counter_Heading_DisplaysCorrectText()
    {
        // Act
        var cut = RenderComponent<Counter>();

        // Assert
        var heading = cut.Find("h1");
        Assert.Equal("Counter", heading.TextContent);
    }
}
