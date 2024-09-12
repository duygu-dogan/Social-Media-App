using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Messages.Commands.DeleteMessage;
internal record DeleteMessageCommand(string? Id): IRequest
{
}
