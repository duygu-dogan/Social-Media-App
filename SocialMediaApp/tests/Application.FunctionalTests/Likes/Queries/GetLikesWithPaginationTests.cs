namespace Application.FunctionalTests.Likes.Queries;

using Application.FunctionalTests;
using FluentAssertions;
using SocialMediaApp.Application.Common.Models;
using SocialMediaApp.Application.Follows.Queries.GetFollowsWithPagination;
using SocialMediaApp.Application.Likes.Queries.GetLikesWithPagination;
using SocialMediaApp.Domain.Entities;
using static Testing;

public class GetLikesWithPaginationTests: BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        await ResetState();

        // Arrange
        var query = new GetLikesWithPaginationQuery();

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
        var result = await SendAsync(new GetLikesWithPaginationQuery { LikerId = user });

        // Assert
        result.Items.Should().BeEmpty();
    }
    [Test]
    public async Task ShouldReturnLikes()
    {
        var user = await RunAsDomainUserAsync("member1", "Test1235!", Array.Empty<string>());
        var user2 = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var posts = new List<Post>
        {
            new Post { Content = "Post 1", CreatedById = Guid.Parse(user2) },
            new Post { Content = "Post 2", CreatedById = Guid.Parse(user2) },
            new Post { Content = "Post 3", CreatedById = Guid.Parse(user2) },
            new Post { Content = "Post 4", CreatedById = Guid.Parse(user2) },
            new Post { Content = "Post 5", CreatedById = Guid.Parse(user2) }
        };

        await AddRangeAsync(posts);

        var likes = posts.Select(p => new Like
        {
            LikerId = Guid.Parse(user),
            PostId = p.Id
        }).ToList();

        await AddRangeAsync(likes);

        var result = await SendAsync(new GetLikesWithPaginationQuery { LikerId = user });

        result.Items.Should().NotBeNull();
        result.Items.Should().HaveCount(5);
        result.Items.Should().BeInDescendingOrder(f => f.Created);
        result.PageNumber.Should().Be(1);
        result.As<PaginatedList<LikeDto>>();
    }
}
