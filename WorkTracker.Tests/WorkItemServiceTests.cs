using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WorkTracker.Api.Data;
using WorkTracker.Api.Dtos;
using WorkTracker.Api.Models;
using WorkTracker.Api.Services;

namespace WorkTracker.Tests;

public class WorkItemServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsWorkItem_WhenItemExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WorkTrackerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new WorkTrackerDbContext(options);

        context.WorkItems.Add(new WorkItem
        {
            Title = "Test item",
            Description = "Test description",
            IsComplete = false,
            CreatedAt = DateTime.UtcNow
        });

        await context.SaveChangesAsync();

        var service = new WorkItemService(context);

        // Act
        var result = await service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test item", result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenItemDoesNotExist()
    {
        // Arrange
        // create an empty in-memory context
        var options = new DbContextOptionsBuilder<WorkTrackerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new WorkTrackerDbContext(options);
        // create the service

        var service = new WorkItemService(context);

        // Act
        // ask for an ID that doesn't exist
        var result = await service.GetByIdAsync(999);

        // Assert
        // verify the result is null
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_CreatesAndReturnsWorkItem()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<WorkTrackerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new WorkTrackerDbContext(options);

        var service = new WorkItemService(context);

        var createWorkItem = new CreateWorkItemRequest();
        createWorkItem.Title = "Test Create - Title";
        createWorkItem.Description = "lorem ipsum dolor sit amet";


        // Act
        var result = await service.CreateAsync(createWorkItem);

        // Assert
        Assert.Equal("Test Create - Title", result.Title);
        Assert.Equal("lorem ipsum dolor sit amet", result.Description);

        var persistedItem = await context.WorkItems.SingleAsync();

    }

    [Fact]
    public async Task UpdateAsync_UpdatesAndReturnsWorkItem()
    {
        // Arrange
        // create isolated in-memory db
        // seed one WorkItem
        // save
        // create service
        // create UpdateWorkItemRequest
        var options = new DbContextOptionsBuilder<WorkTrackerDbContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString())
          .Options;

        using var context = new WorkTrackerDbContext(options);

        context.WorkItems.Add(new WorkItem
        {
            Title = "Test item",
            Description = "Test description",
            IsComplete = false,
            CreatedAt = DateTime.UtcNow
        });

        await context.SaveChangesAsync();

        var persistedItem = await context.WorkItems.SingleAsync();

        var service = new WorkItemService(context);



        // Act
        // call UpdateAsync(existingId, request)

        var updatedWorkItem = new UpdateWorkItemRequest();
        updatedWorkItem.Title = "Test Update - Title";
        updatedWorkItem.Description = "lorem ipsum dolor sit amet";
        updatedWorkItem.IsComplete = true;


        var result = await service.UpdateAsync(persistedItem.Id, updatedWorkItem);


        context.ChangeTracker.Clear();

        var persistedItemAfterUpdate = await context.WorkItems.SingleAsync();

        // Assert
        // result is not null
        Assert.NotNull(result);

        // 1. Did the service return the right response?
        Assert.Equal(updatedWorkItem.Title, result.Title);
        Assert.Equal(updatedWorkItem.Description, result.Description);
        Assert.Equal(updatedWorkItem.IsComplete, result.IsComplete);

        // 2. Did the data actually persist?
        context.ChangeTracker.Clear();

        Assert.Equal(updatedWorkItem.Title, persistedItemAfterUpdate.Title);
        Assert.Equal(updatedWorkItem.Description, persistedItemAfterUpdate.Description);
        Assert.Equal(updatedWorkItem.IsComplete, persistedItemAfterUpdate.IsComplete);
    }

    [Fact(Skip = "Not implemented yet")]
    public async Task UpdateAsync_ReturnsNull_WhenItemDoesNotExist()
    {
    }

    [Fact(Skip = "Not implemented yet")]
    public async Task DeleteAsync_DeletesExistingWorkItem()
    {
    }

    [Fact(Skip = "Not implemented yet")]
    public async Task DeleteAsync_ReturnsNull_WhenItemDoesNotExist()
    {
    }

    [Fact(Skip = "Not implemented yet")]
    public async Task GetAllAsync_ReturnsAllWorkItems()
    {
    }

    [Fact(Skip = "Not implemented yet")]
    public async Task GetAllAsync_ReturnsEmptyList_WhenNoItemsExist()
    {
    }

    [Fact(Skip = "Not implemented yet")]
    public async Task CreateAsync_SetsDefaultValues()
   {
       // For CreateAsync_SetsDefaultValues, I’d specifically verify things like

        //        IsComplete == false
        //CreatedAt was set
        //Id was generated

        //That gives you a good compact set of service tests to come back to later without slowing down the main learning path.
    }
}