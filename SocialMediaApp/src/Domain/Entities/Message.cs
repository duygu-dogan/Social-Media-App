using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Message:AuthorAuditableEntity
{
    public string? Content { get; set; }
    public Guid ToUserId { get; set; }
    public User? ToUser { get; set; }
    public Guid ConversationId { get; set; }
    public Conversation? Conversation { get; set; }
    public bool IsRead { get; set; }
    public Media? Media { get; set; } = null;
    public Guid? MediaId { get; set; }
}
