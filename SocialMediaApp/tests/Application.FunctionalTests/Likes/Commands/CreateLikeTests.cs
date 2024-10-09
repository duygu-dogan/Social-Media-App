namespace Application.FunctionalTests.Likes.Commands;

using Application.FunctionalTests;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Follows.Commands.CreateFollow;
using SocialMediaApp.Application.Likes.Commands.CreateLike;
using SocialMediaApp.Domain.Entities;
using static Testing;

internal class CreateLikeTests: BaseTestFixture
{
    [Test]
    public async Task ShouldThrowNotFoundException_WhenPostIdIsNull()
    {
        var command = new CreateLikeCommand();

        await FluentActions.Invoking(() => SendAsync(command))
             .Should()
             .ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldThrowForbiddenAccessException_WhenLikerNotFound()
    {
        var likerId = await RunAsUserAsync("member2", "Test1235!", Array.Empty<string>());
        var likedId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());

        var post = new Post
        {
            Id = Guid.NewGuid(),
            Content = "Test Content",
            Created = DateTime.Now,
            CreatedById = Guid.Parse(likedId)
        };
        await AddAsync(post);

        var command = new CreateLikeCommand { PostId = post.Id.ToString()};

        await FluentActions.Invoking(() => SendAsync(command))
            .Should()
            .ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldCreateLike_WhenPostAndUserFound()
    {
        await ResetState();

        var likedId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var likerId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var post = new Post
        {
            Id = Guid.NewGuid(),
            Content = "Test Content",
            Created = DateTime.Now,
            CreatedById = Guid.Parse(likedId)
        };
        await AddAsync(post);

        var command = new CreateLikeCommand { PostId = post.Id.ToString() };

        var result = await SendAsync(command);
        var like = await FirstOrDefaultAsync<Like>(like => like.PostId == post.Id && like.LikerId == Guid.Parse(likerId));

        like.Should().NotBeNull();
        result.Should().Be(Unit.Value);
        result.Should().NotBeNull();
    }
    [Test]
    public async Task ShouldNotCreateDuplicateLike_WhenAlreadyLiked()
    {
        var likedId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var likerId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var post = new Post
        {
            Id = Guid.NewGuid(),
            Content = "Test Content",
            Created = DateTime.Now,
            CreatedById = Guid.Parse(likedId)
        };
        await AddAsync(post);
        
        await AddAsync(new Like
        {
            PostId = post.Id,
            LikerId = Guid.Parse(likerId)
        });

        var result = await SendAsync(new CreateLikeCommand { PostId = post.Id.ToString() });
        var like = await FindAsync<Like>(new object[] { Guid.Parse(likerId), post.Id });

        like.Should().NotBeNull();
        result.Should().Be(Unit.Value);
    }
}
