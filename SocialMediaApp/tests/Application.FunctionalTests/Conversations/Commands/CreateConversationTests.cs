namespace Application.FunctionalTests.Conversations.Commands;

using FluentAssertions;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Conversations.Commands.CreateConversation;
using SocialMediaApp.Domain.Entities;
using static Testing;
public class CreateConversationTests: BaseTestFixture
{
    [Test]
    public async Task ShouldThrowValidationEx_WhenNoMembers()
    {
        var command = new CreateConversationCommand();
        await FluentActions.Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldThrowNotFoundEx_WhenAnyUserFound()
    {
        var command = new CreateConversationCommand()
        {
            Members = new List<string> { "nonexistent-user1", "nonexistent-user1" }
        };

        await FluentActions.Invoking(() => SendAsync(command))
        .Should()
            .ThrowAsync<NotFoundException>()
            .Where(ex => ex.Message.Contains("nonexistent-user1") || ex.Message.Contains("nonexistent-user2"));
    }

    [Test]
    public async Task ShouldCreateConversation_WhenAllUsersFound()
    {
        await ResetState();

        var member1Id = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());
        var member2Id = await RunAsDomainUserAsync("member2", "Test1234!", Array.Empty<string>());

        var command = new CreateConversationCommand()
        {
            Members = new List<string> { member1Id, member2Id }
        };
        var id = await SendAsync(command);

        var conversation = await FirstOrDefaultAsync<Conversation>(c => c.Id.ToString() == id, include: c => c.Members!);

        conversation.Should().NotBeNull();
        conversation!.Members.Should().HaveCount(2);
        conversation!.Members.Should().Contain(u => u.Id.ToString() == member1Id);
        conversation!.Members.Should().Contain(u => u.Id.ToString() == member1Id);
    }
}
