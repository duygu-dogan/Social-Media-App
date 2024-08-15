using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Events;
public class RepostCreatedEvent:BaseEvent
{
    public RePost? RePost { get; set; }

    public RepostCreatedEvent(RePost? rePost) => RePost = rePost;
}
