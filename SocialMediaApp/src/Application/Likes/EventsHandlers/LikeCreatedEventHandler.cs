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
public class LikeCreatedEventHandler : INotificationHandler<LikeCreatedEvent>
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
        var like = await _context.Likes.Where(l => l.Id == notification.Like!.Id).Include(l => l.Post)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        
        var alreadyNotified = await _context.Notifications.AnyAsync(
            n => n.ForUserId == like!.Post!.CreatedById && n.CreatedById == like.LikerId && n.Type == Domain.Entities.NotificationType.Like, cancellationToken);

        if (alreadyNotified)
            return;

        var notif = new Notification
        {   
            CreatedById = like!.LikerId,
            ForUserId = like!.Post!.CreatedById,
            Type = NotificationType.Like
        };
        await _context.Notifications.AddAsync(notif, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("SocialMediaApp Domain Event: {DomainEvent}", notification.GetType().Name);
    }
}
