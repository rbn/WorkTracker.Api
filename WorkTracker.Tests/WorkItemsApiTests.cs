using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using WorkTracker.Api.Dtos;

namespace WorkTracker.Tests;

public class WorkItemsApiTests
{
    [Fact]
    public async Task GetWorkItems_ReturnsOk()
    {
        // Arrange
        await using var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/workitems");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateWorkItem_ReturnsBadRequest_WhenTitleIsMissing()
    {
        await using var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var request = new
        {
            description = "Missing title"
        };

        var response = await client.PostAsJsonAsync("/api/workitems", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task CreateWorkItem_ReturnsCreated_WhenRequestIsValid()
    {
        await using var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var request = new
        {
            title = "Integration test item",
            description = "Created through the API"
        };

        var response = await client.PostAsJsonAsync("/api/workitems", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<WorkItemResponse>();

        Assert.NotNull(created);
        Assert.Equal("Integration test item", created.Title);
    }
}