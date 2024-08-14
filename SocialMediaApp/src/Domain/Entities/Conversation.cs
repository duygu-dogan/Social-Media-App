using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Conversation: BaseAuditableEntity
{
    public IEnumerable<User>? Members { get; set; }
    public IEnumerable<Message>? Messages { get; set; }
}
