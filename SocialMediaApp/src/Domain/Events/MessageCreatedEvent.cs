using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Events;
public class MessageCreatedEvent: BaseEvent
{
    public Message? Message { get; set; }

    public MessageCreatedEvent(Message? message)
    {
        Message = message;
    }
}
