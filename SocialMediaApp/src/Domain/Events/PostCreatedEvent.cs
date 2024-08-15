using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Events;
public class PostCreatedEvent: BaseEvent
{
    public Post? Post { get; set; }
    public PostCreatedEvent(Post? post) => Post = post;
}
