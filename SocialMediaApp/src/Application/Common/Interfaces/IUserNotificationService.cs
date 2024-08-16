using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Common.Interfaces;
public interface IUserNotificationService
{
    void SendNotification(Notification notification);
}
