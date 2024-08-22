using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Conversations.Commands.DeleteConversation;
public record DeleteConversationCommand(string id) : IRequest;
