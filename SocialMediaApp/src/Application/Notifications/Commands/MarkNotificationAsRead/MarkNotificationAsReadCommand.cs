using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Notifications.Commands.MarkNotificationAsRead;
internal record MarkNotificationAsReadCommand(string? Id): IRequest;
