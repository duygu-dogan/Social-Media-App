using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Common.Interfaces;

namespace SocialMediaApp.Application.Notifications.Queries.GetNotifications;
internal class GetNotificationsQueryHandler: IRequestHandler<GetNotificationsQuery, NotificationsVM>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public GetNotificationsQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<NotificationsVM> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.DomainUsers.FindAsync(_user.Id, cancellationToken);
        
        if (user == null) 
        {
            throw new ForbiddenAccessException();   
        }

        var query = _context.Notifications
            .AsNoTracking()
            .Include(n => n.Post)
            .Where(n => n.ForUserId == user.Id);

        var notifications = await query.OrderByDescending(query => query.Created)
            .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
            .Take(request.Count ?? 20)
            .ToListAsync(cancellationToken);

        var totalUnread = await _context.Notifications
            .Where(n => n.ForUserId == user.Id && !n.IsRead)
            .CountAsync(cancellationToken);

        return new NotificationsVM
        {
            Notifications = notifications,
            TotalUnread = totalUnread
        };
    }
}
