namespace Application.FunctionalTests.Follows.Queries;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.FunctionalTests;
using FluentAssertions;
using SocialMediaApp.Application.Common.Models;
using SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
using SocialMediaApp.Application.Follows.Queries.GetFollowsWithPagination;
using SocialMediaApp.Domain.Entities;
using static Testing;

internal class GetFollowsWithPaginationTests: BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await ResetState();

        // Arrange
        var query = new GetFollowsWithPaginationQuery();

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
        var user = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());

        // Act
        var result = await SendAsync(new GetFollowsWithPaginationQuery { FollowerId = user });

        // Assert
        result.Items.Should().BeEmpty();
    }
    [Test]
    public async Task ShouldReturnFollows()
    {
        var user = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());

        var otherUsers = Enumerable.Range(1, 5).Select(i => new User
        {
            Id = Guid.NewGuid(),
            ApplicationUserId = Guid.NewGuid(),
            FullName = $"Other User {i}"
        }).ToList();

        await AddRangeAsync(otherUsers);

        var follows = otherUsers.Select(u => new Follow
        {
            FollowerId = Guid.Parse(user),
            FollowedId = u.Id
        }).ToList();

        await AddRangeAsync(follows);

        var result = await SendAsync(new GetFollowsWithPaginationQuery { FollowerId = user });

        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(5);
        result.Items.Should().BeInDescendingOrder(f => f.Created);
        result.PageNumber.Should().Be(1);
        result.As<PaginatedList<FollowDto>>();
    }
}
