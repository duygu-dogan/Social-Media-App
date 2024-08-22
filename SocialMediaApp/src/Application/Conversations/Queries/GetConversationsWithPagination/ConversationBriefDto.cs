using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public class ConversationBriefDto
{
    public string? Id { get; set; }
    public string? Content { get; set; }
    public string? SenderId { get; set; }
    public string? ReciepentId { get; set; }
    public DateTime Created { get; set; }
}
