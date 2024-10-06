namespace Application.FunctionalTests.Follows.Queries;

using Application.FunctionalTests;
using FluentAssertions;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Follows.Commands.CreateFollow;
using SocialMediaApp.Application.Follows.Queries.GetFollowSuggestions;
using SocialMediaApp.Domain.Entities;
using static Testing;

public class GetFollowSuggestionsTests: BaseTestFixture
{
    [Test]
    public async Task ShouldThrowForbiddenAccessEx_WhenUserNotFound()
    {
        var user = await RunAsUserAsync("userTest", "Test1234!", Array.Empty<string>());
        var query = new GetFollowSuggestionsQuery();

        var action = () => SendAsync(query);

        await action.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldGetFollowSuggestions_WhenRequestedCountGiven()
    {
        // Arrange
        await RunAsDomainUserAsync("TestUser", "Test1234!", Array.Empty<string>());

        await RunAsDomainUserAsync("userTest1", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest2", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest3", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest4", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest5", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest6", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest7", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest8", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest9", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest10", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest11", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest12", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest13", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest14", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest15", "Test12345!", Array.Empty<string>());

        var query = new GetFollowSuggestionsQuery
        {
            Count = 5
        };

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(5);
    }
    [Test]
    public async Task ShouldGetFollowSuggestions_WhenRequestedCountNotGiven()
    {
        // Arrange
        await RunAsDomainUserAsync("TestUser", "Test1234!", Array.Empty<string>());

        await RunAsDomainUserAsync("userTest1", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest2", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest3", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest4", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest5", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest6", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest7", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest8", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest9", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest10", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest11", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest12", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest13", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest14", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest15", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest16", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest17", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest18", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest19", "Test12345!", Array.Empty<string>());
        await RunAsDomainUserAsync("userTest20", "Test12345!", Array.Empty<string>());

        var query = new GetFollowSuggestionsQuery();

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(10);
    }

    [Test]
    public async Task ShouldReturnCorrectNumberOfSuggestions_IfNotFollowed()
    {
        var user1 = await RunAsDomainUserAsync("TestUser1", "Test1234!", Array.Empty<string>());
        var user2 = await RunAsDomainUserAsync("TestUser2", "Test1234!", Array.Empty<string>());
        var user3 = await RunAsDomainUserAsync("TestUser3", "Test1234!", Array.Empty<string>());
        var user4 = await RunAsDomainUserAsync("TestUser4", "Test1234!", Array.Empty<string>());

        await AddAsync(new Follow { FollowerId = Guid.Parse(user4), FollowedId = Guid.Parse(user1) });

        var result = await SendAsync(new GetFollowSuggestionsQuery());

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }
}
