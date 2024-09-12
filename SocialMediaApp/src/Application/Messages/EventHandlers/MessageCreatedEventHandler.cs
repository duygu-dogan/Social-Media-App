using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;

namespace SocialMediaApp.Application.Messages.EventHandlers;
internal class MessageCreatedEventHandler : INotificationHandler<MessageCreatedEvent>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<MessageCreatedEventHandler> _logger;

    public MessageCreatedEventHandler(IApplicationDbContext context, ILogger<MessageCreatedEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
    {
        var message = notification.Message;

        var alreadyNotified = await _context.Notifications.AnyAsync(
            n => n.ForUserId == message!.ToUserId && n.CreatedBy!.Id == message.CreatedBy!.Id && n.Type == Domain.Entities.NotificationType.Message, cancellationToken);
        if (alreadyNotified)
            return;

        var notif = new Notification
        {
            ForUserId = message!.ToUserId,
            Type = NotificationType.Message,
            CreatedBy = message.CreatedBy
        };
        await _context.Notifications.AddAsync(notif, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("SocialMediaApp Domain Event: {DomainEvent}", notification.GetType().Name);
    }
}
