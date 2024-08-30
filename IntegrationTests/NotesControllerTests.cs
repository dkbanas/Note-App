using System.Text;
using Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using FluentAssertions;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Text.Json;
namespace IntegrationTests;


public class NotesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private HttpClient _client;

    public NotesControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));
                });
            })
            .CreateClient();
    }
    [Theory]
    [InlineData("pageIndex=1&pageSize=10")]
    [InlineData("pageIndex=1&pageSize=10&sort=date-reverse")]
    [InlineData("pageIndex=2&pageSize=10&sort=date-reverse")]
    [InlineData("pageIndex=1&pageSize=10&sort=date-reverse&search=Plan")]
    public async Task GetNotes_WithQueryParams_ReturnsOK(string queryparams)
    {
        //Act
        var response = await _client.GetAsync("/Notes?" + queryparams);
        //Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetNoteById_WhenNoteExist_ReturnsOK()
    {
        //Act
        var response = await _client.GetAsync("/Notes/1");
        //Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetNoteById_WhenNoteNotExist_ReturnsNotFound()
    {
        //Act
        var response = await _client.GetAsync("/Notes/999");
        //Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task CreateNote_ValidNote_ReturnsOK()
    {
        // Arrange
        var noteDto = new 
        {
            Title = "Integration test note",
            Description = "This is note created during integration tests"
        };

        var content = new StringContent(JsonSerializer.Serialize(noteDto), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/Notes", content);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task UpdateNoteById_ValidNote_ReturnsOK()
    {
        // Arrange
        var noteId = 14;
        var updatedNoteDto = new
        {
            Title = "Updated Title",
            Description = "Updated Description"
        };

        var content = new StringContent(JsonSerializer.Serialize(updatedNoteDto), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PutAsync($"/Notes/{noteId}", content);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteNoteById_ValidNote_ReturnsOK()
    {
        var noteId = 16;
        // Act
        var response = await _client.DeleteAsync($"/Notes/{noteId}");
        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}