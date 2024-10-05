using FluentAssertions;
using SocialMediaApp.Application.Conversations.Commands.CreateConversation;
using SocialMediaApp.Application.Messages.Commands.CreateMessage;
using SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
using static Application.FunctionalTests.Testing;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Application.Common.Exceptions;

namespace Application.FunctionalTests.Conversations.Queries;
public class GetConversationTests: BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await ResetState();

        // Arrange
        var query = new GetConversationsWithPaginationQuery();

        // Act
        var action = () => SendAsync(query);

        // Assert

        await action.Should().ThrowAsync<SocialMediaApp.Application.Common.Exceptions.ValidationException>();
        
    }
    [Test]
    public async Task ShouldReturnEmptyList()
    {
        await ResetState();

        // Arrange
        var member1 = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());

        // Act
        var result = await SendAsync(new GetConversationsWithPaginationQuery { UserId = member1 });

        // Assert
        result.Items.Should().BeEmpty();
    }
    [Test]
    public async Task ShouldReturnConversation()
    {
        await ResetState();

        // Arrange
        var member1 = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());
        var member2 = await RunAsDomainUserAsync("member2", "Test1234!", Array.Empty<string>());

        var conversationId = await SendAsync(new CreateConversationCommand()
        {
            Members = new List<string> { member1, member2 }
        });

      await SendAsync(new CreateMessageCommand()
        {
            ConversationId = conversationId,
            ToUserId = member1,
            Content = "Hello"
        });

        // Act
        var result = await SendAsync(new GetConversationsWithPaginationQuery { UserId = member1 });

        // Assert
        result.Items.Should().HaveCount(1);
        result.Items.First().Members.Should().HaveCount(2);
        result.Items.First().LastMessage!.Content.Should().Be("Hello");
    }
    [Test]
    public async Task ShouldReturnAllConversations()
    {
        await ResetState();

       // Arrange
        var member1 = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());
        var member2 = await RunAsDomainUserAsync("member2", "Test1234!", Array.Empty<string>());

        await SendAsync(new CreateConversationCommand()
        {
            Members = new List<string> { member1, member2 }
        });


        // Act

        var result = await SendAsync(new GetConversationsWithPaginationQuery { UserId = member1 });

        // Assert
        result.Items.Should().HaveCount(1);
        result.Items.First().Members.Should().HaveCount(2);
    }
}
