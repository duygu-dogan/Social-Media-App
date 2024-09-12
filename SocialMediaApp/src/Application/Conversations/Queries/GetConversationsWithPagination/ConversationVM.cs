using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public class ConversationVM
{    
    public IReadOnlyCollection<UserDto> Members { get; set; } = Array.Empty<UserDto>();
    public IReadOnlyCollection<MessageDto> Messages { get; set; } = Array.Empty<MessageDto>();
}
