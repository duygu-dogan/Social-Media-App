using System;
using System.Collections.Generic;
using System.Linq;
 using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Follows.Commands.CreateFollow;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Infrastructure.Data;
using static Application.FunctionalTests.Testing;
namespace Application.FunctionalTests.Follows.Commands;
public class CreateFollowTests: BaseTestFixture
{
    [Test]
    public async Task ShouldThrowBadRequestException_WhenFollowedIdIsNull()
    {
        var command = new CreateFollowCommand();

        await FluentActions.Invoking(() => SendAsync(command))
             .Should()
             .ThrowAsync<BadRequestException>();
    }

    [Test]
    public async Task ShouldThrowForbiddenAccessException_WhenFollowerNotFound()
    {
        var followedId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var followerId = await RunAsUserAsync("member2", "Test1235!", Array.Empty<string>());

        var command = new CreateFollowCommand
        {
            FollowedId = followedId
        };

        await FluentActions.Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldThrowNotFoundException_WhenFollowedNotFound()
    {
        var followedId = await RunAsUserAsync("member1", "Test1234!", Array.Empty<string>());
        var followerId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var command = new CreateFollowCommand
        {
            FollowedId = followedId
        };

        await FluentActions.Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldCreateFollow_WhenAllUsersFound()
    {
        var followedId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var followerId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var command = new CreateFollowCommand
        {
            FollowedId = followedId
        };

        var result = await SendAsync(command);
        result.Should().Be(Unit.Value);
        result.Should().NotBeNull();
    }
    [Test]
    public async Task ShouldNotCreateDuplicateFollow_WhenAlreadyFollowed()
    {
        var followedId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var followerId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        await AddAsync(new Follow
        {
            FollowerId = Guid.Parse(followerId),
            FollowedId = Guid.Parse(followedId)
        });

        var command = new CreateFollowCommand
        {
            FollowedId = followedId
        };
        var result = await SendAsync(command);

        var follow = await FirstOrDefaultAsync<Follow>(f => f.FollowedId == Guid.Parse(followedId) && f.FollowerId == Guid.Parse(followerId));
        follow.Should().NotBeNull();
        result.Should().Be(Unit.Value);
    }
}
