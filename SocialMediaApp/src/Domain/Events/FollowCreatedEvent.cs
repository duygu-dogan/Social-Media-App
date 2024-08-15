using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Events;
public class FollowCreatedEvent: BaseEvent
{
    public Follow? Follow { get; set; }

    public FollowCreatedEvent(Follow? follow)
    {
        Follow = follow;
    }
}
