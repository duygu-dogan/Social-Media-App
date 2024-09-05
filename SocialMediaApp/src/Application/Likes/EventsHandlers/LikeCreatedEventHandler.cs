using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;

namespace SocialMediaApp.Application.Likes.EventsHandlers;
internal class LikeCreatedEventHandler : INotificationHandler<LikeCreatedEvent>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<LikeCreatedEventHandler> _logger;

    public LikeCreatedEventHandler(IApplicationDbContext context, ILogger<LikeCreatedEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(LikeCreatedEvent notification, CancellationToken cancellationToken)
    {
        var like = notification.Like;
        
        var alreadyNotified = await _context.Notifications.AnyAsync(
            n => n.ForUserId == like!.Post!.CreatedBy!.Id && n.CreatedBy!.Id == like.CreatedBy!.Id && n.Type == Domain.Entities.NotificationType.Like, cancellationToken);

        if (alreadyNotified)
            return;

        var notif = new Notification
        {   
            ForUserId = like!.Post!.CreatedBy!.Id,
            Type = NotificationType.Like
        };
        await _context.Notifications.AddAsync(notif, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("SocialMediaApp Domain Event: {DomainEvent}", notification.GetType().Name);
    }
}
