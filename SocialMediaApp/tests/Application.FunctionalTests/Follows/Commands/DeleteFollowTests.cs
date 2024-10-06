namespace Application.FunctionalTests.Follows.Commands;

using Application.FunctionalTests;
using FluentAssertions;
using SocialMediaApp.Application.Conversations.Commands.CreateConversation;
using SocialMediaApp.Application.Conversations.Commands.DeleteConversation;
using SocialMediaApp.Application.Follows.Commands.CreateFollow;
using SocialMediaApp.Application.Follows.Commands.DeleteFollow;
using SocialMediaApp.Domain.Entities;
using static Testing;

public class DeleteFollowTests: BaseTestFixture
{
    [Test]
    public async Task ShouldThrowNotFoundEx_WhenNoFollowFound()
    {
        var followed = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var follower = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var command = new DeleteFollowCommand
        {
            FollowedId = followed,
            FollowerId = follower
        };
        await FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteFollow_WhenIdIsValid()
    {
        var member1 = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());
        var member2 = await RunAsDomainUserAsync("member2", "Test1234!", Array.Empty<string>());

        await SendAsync(new CreateFollowCommand()
        {
            FollowedId = member1
        });

        await SendAsync(new DeleteFollowCommand
        {
            FollowedId = member1,
            FollowerId = member2
        });

        var follow = await FindAsync<Follow>(new object[] {Guid.Parse(member1), Guid.Parse(member2)});
        follow.Should().BeNull();
    }
}
