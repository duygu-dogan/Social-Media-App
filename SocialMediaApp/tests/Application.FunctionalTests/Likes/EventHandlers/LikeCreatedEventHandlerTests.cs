using System;
namespace Application.FunctionalTests.Likes.EventHandlers;

using Application.FunctionalTests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialMediaApp.Application.Likes.EventsHandlers;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;
using SocialMediaApp.Infrastructure.Data;

using static Testing;

public class LikeCreatedEventHandlerTests:BaseTestFixture
{
    private ApplicationDbContext _context;
    private ILogger<LikeCreatedEventHandler> _logger;
    private LikeCreatedEventHandler _handler;
    private ITestDatabase _testDatabase;

    [SetUp]
    public async Task SetUp()
    {
        _testDatabase = await TestDatabaseFactory.CreateAsync();

        var connectionString = (_testDatabase as PostgreSqlTestDatabase)?.GetConnectionString();

        if (connectionString == null)
        {
            throw new InvalidOperationException("Could not retrieve the connection string.");
        }

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        _context = new ApplicationDbContext(options);

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        _logger = loggerFactory.CreateLogger<LikeCreatedEventHandler>();

        _handler = new LikeCreatedEventHandler(_context, _logger);
    }

    [Test]
    public async Task ShouldNotCreateNotification_IfAlreadyNotified()
    {
        var likerId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var likedId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var post = new Post
        {
            Id = Guid.NewGuid(),
            CreatedById = Guid.Parse(likedId),
            Content = "Test Post",
            Created = DateTime.Now
        };
        var like = new Like
        {
            Post = post,
            LikerId = Guid.Parse(likerId),

        };
        await AddAsync(like);

        var existingNotif = new Notification
        {
            ForUserId = Guid.Parse(likedId),
            CreatedById = Guid.Parse(likerId),
            Type = NotificationType.Like
        };
        await AddAsync(existingNotif);

        var notifEvent = new LikeCreatedEvent(like);

        //Act
        await _handler.Handle(notifEvent, new CancellationToken());

        var notifCount = await CountAsync<Notification>();
        notifCount.Should().Be(1);
    }
    [Test]
    public async Task ShouldCreateNotification_IfNotNotified()
    {
        var likerId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var likedId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var post = new Post
        {
            Id = Guid.NewGuid(),
            CreatedById = Guid.Parse(likedId),
            Content = "Test Post",
            Created = DateTime.Now
        };
        var like = new Like
        {
            Post = post,
            LikerId = Guid.Parse(likerId),

        };
        await AddAsync(like);

        var notifEvent = new LikeCreatedEvent(like);

        //Act
        await _handler.Handle(notifEvent, new CancellationToken());

        var notification = await FirstOrDefaultAsync<Notification>(n => n.CreatedById.ToString() == likerId && n.ForUserId.ToString() == likedId && n.Type == NotificationType.Like);
        notification.Should().NotBeNull();
    }
    [TearDown]
    public async Task TearDown()
    {
        if (_testDatabase != null && _context != null)
        {
            await _testDatabase.DisposeAsync();
            await _context.DisposeAsync();
        }
    }

}
