using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Conversations.Commands.CreateConversation;
public record CreateConversationCommand : IRequest<string>
{
    public IEnumerable<string>? Members { get; init; }
}
