namespace Application.FunctionalTests.Follows.EventHandler;

using System;
using System.Threading.Tasks;
using Application.FunctionalTests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialMediaApp.Application.Follows.EventHandlers;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;
using SocialMediaApp.Infrastructure.Data;
using static Application.FunctionalTests.Testing;
using static LoggerTest;

public class FollowCreatedEventHandlerTests: BaseTestFixture
{
    private ApplicationDbContext _context;
    private ILogger<FollowCreatedEventHandler> _logger;
    private FollowCreatedEventHandler _handler;
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
        _logger = loggerFactory.CreateLogger<FollowCreatedEventHandler>();

        _handler = new FollowCreatedEventHandler(_logger, _context);
    }

    [Test]
    public async Task ShouldNotCreateNotification_IfAlreadyNotified()
    {
        var followerId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var followedId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var existingNotif = new Notification
        {
            ForUserId = Guid.Parse(followedId),
            CreatedById = Guid.Parse(followerId),
            Type = NotificationType.Follow
        };
        await AddAsync(existingNotif);
        
        var follow = new Follow
        {
            FollowerId = Guid.Parse(followerId),
            FollowedId = Guid.Parse(followedId)
        };
        var notifEvent = new FollowCreatedEvent(follow);
        
        //Act
        await _handler.Handle(notifEvent, new CancellationToken());

        var notifCount = await CountAsync<Notification>();
        notifCount.Should().Be(1);       
    }
    [Test]
    public async Task ShouldCreateNotification_IfNotNotified()
    {
        var followerId = await RunAsDomainUserAsync("member1", "Test1234!", Array.Empty<string>());
        var followedId = await RunAsDomainUserAsync("member2", "Test1235!", Array.Empty<string>());

        var follow = new Follow
        {
            FollowedId = Guid.Parse(followedId),
            FollowerId = Guid.Parse(followerId)
        };
        var notifEvent = new FollowCreatedEvent(follow);

        //Act
        await _handler.Handle(notifEvent, new CancellationToken());

        var notification = await FirstOrDefaultAsync<Notification>(n => n.CreatedById.ToString() == followerId && n.ForUserId.ToString() == followedId && n.Type == NotificationType.Follow);
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
