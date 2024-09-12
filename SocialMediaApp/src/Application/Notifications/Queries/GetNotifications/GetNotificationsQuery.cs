using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Notifications.Queries.GetNotifications;
internal class GetNotificationsQuery: IRequest<NotificationsVM>
{
    public int? Count { get; set; }
}
