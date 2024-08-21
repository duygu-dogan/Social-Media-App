using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Events;
public class ConversationCreatedEvent: BaseEvent
{
    public Conversation? Conversation { get; set; }

    public ConversationCreatedEvent(Conversation conversation)
    {
        Conversation = conversation;
    }
}
