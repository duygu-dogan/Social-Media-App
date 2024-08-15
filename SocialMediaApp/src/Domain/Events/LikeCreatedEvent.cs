using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Events;
public class LikeCreatedEvent: BaseEvent
{
    public Like? Like { get; set; }

    public LikeCreatedEvent(Like? like) => Like = like;
}
