using Microsoft.Extensions.Logging;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;

namespace SocialMediaApp.Application.Follows.EventHandlers;
public class FollowCreatedEventHandler : INotificationHandler<FollowCreatedEvent>
{
    private readonly ILogger<FollowCreatedEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public FollowCreatedEventHandler(ILogger<FollowCreatedEventHandler> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(FollowCreatedEvent notification, CancellationToken cancellationToken)
    {
        var follow = notification.Follow;

        var alreadyNotified = await _context.Notifications.AnyAsync(
            n => n.ForUserId == follow!.FollowedId && n.CreatedById == follow.FollowerId && n.Type == Domain.Entities.NotificationType.Follow, cancellationToken);
        if (alreadyNotified)
            return;

        var notif = new Notification
        {
            ForUserId = follow!.FollowedId,
            Type = NotificationType.Follow,
        };

        _logger.LogInformation("SocialMediaApp Domain Event: {DomainEvent}", notification.GetType().Name);
        _context.Notifications.Add(notif);
        await _context.SaveChangesAsync(cancellationToken);

    }
}
