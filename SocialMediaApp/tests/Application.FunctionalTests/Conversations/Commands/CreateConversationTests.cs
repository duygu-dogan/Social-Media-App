namespace Application.FunctionalTests.Conversations.Commands;

using FluentAssertions;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Conversations.Commands.CreateConversation;
using static Testing;
public class CreateConversationTests: BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMembers()
    {
        var command = new CreateConversationCommand();
        await FluentActions.Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }
}
