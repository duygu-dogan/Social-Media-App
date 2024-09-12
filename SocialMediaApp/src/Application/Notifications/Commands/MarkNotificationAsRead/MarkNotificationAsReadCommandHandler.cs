using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Notifications.Commands.MarkNotificationAsRead;
internal class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public MarkNotificationAsReadCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var notif = await _context.Notifications
            .Include(n => n.ForUser)
            .FirstOrDefaultAsync(n => n.Id.ToString() == request.Id, cancellationToken);

        if(notif == null)
        {
            throw new NotFoundException(nameof(Notification), request.Id!);
        }

        if(notif.ForUser!.Id.ToString() != _user.Id)
        {
            throw new ForbiddenAccessException();
        }
        notif.IsRead = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

}
