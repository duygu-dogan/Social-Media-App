using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SocialMediaApp.Application.Conversations.Commands.CreateConversation;
using SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
using static Application.FunctionalTests.Testing;

namespace Application.FunctionalTests.Conversations.Queries;
public class GetConversationTests: BaseTestFixture
{
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
