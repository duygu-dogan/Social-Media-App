using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SocialMediaApp.Application.Conversations.Commands.CreateConversation;
using SocialMediaApp.Application.Conversations.Commands.DeleteConversation;
using SocialMediaApp.Domain.Entities;
using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Conversations.Commands;
public class DeleteConversationTests: BaseTestFixture
{
    [Test]
    public async Task ShouldThrowNotFoundEx_WhenNoConvFound()
    {
        var command = new DeleteConversationCommand("311811c5-a9d2-493e-b0ba-2a80d1841c1c");
        await FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteConversation_WhenIdIsValid()
    {
        var member1 = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());
        var member2 = await RunAsDomainUserAsync("member2", "Test1234!", Array.Empty<string>());

        var conversationId = await SendAsync(new CreateConversationCommand()
        {
            Members = new List<string> { member1, member2 }
        });

        await SendAsync(new DeleteConversationCommand(conversationId));

        var conversation = await FindAsync<Conversation>(Guid.Parse(conversationId));
        conversation.Should().BeNull();
    }
}
