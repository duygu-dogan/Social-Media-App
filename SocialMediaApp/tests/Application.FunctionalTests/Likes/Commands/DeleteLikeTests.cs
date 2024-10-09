namespace Application.FunctionalTests.Likes.Commands;

using Application.FunctionalTests;
using FluentAssertions;
using SocialMediaApp.Application.Follows.Commands.CreateFollow;
using SocialMediaApp.Application.Follows.Commands.DeleteFollow;
using SocialMediaApp.Application.Likes.Commands.DeleteLike;
using SocialMediaApp.Domain.Entities;
using static Testing;

internal class DeleteLikeTests: BaseTestFixture
{
    [Test]
    public async Task ShouldThrowNotFoundEx_WhenNoLikeFound()
    {
        var liked = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var liker = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var post = new Post
        {
            Id = Guid.NewGuid(),
            Content = "Test Content",
            Created = DateTime.Now,
            CreatedById = Guid.Parse(liked)
        };
        await AddAsync(post);

        var like = new Like
        {
            PostId = post.Id,
            LikerId = Guid.Parse(liker)
        };

        var command = new DeleteLikeCommand
        {
            PostId = post.Id.ToString(),
            LikerId = liker
        };
        await FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteLike_WhenIdIsValid()
    {
        var liked = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var liker = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var post = new Post
        {
            Id = Guid.NewGuid(),
            Content = "Test Content",
            Created = DateTime.Now,
            CreatedById = Guid.Parse(liked)
        };
        await AddAsync(post);

        await AddAsync(new Like
        {
            PostId = post.Id,
            LikerId = Guid.Parse(liker)
        }
);

        var command = new DeleteLikeCommand
        {
            PostId = post.Id.ToString(),
            LikerId = liker
        };

        var like = await FindAsync<Like>(new object[] { Guid.Parse(liked), post.Id });
        like.Should().BeNull();
    }
}
