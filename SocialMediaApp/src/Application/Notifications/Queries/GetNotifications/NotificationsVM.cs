using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Notifications.Queries.GetNotifications;
internal class NotificationsVM
{
    public IEnumerable<NotificationDto>? Notifications { get; set; }
    public int TotalUnread { get; set; }
}
