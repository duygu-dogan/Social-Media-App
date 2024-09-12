using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Messages.Commands.CreateMessage;
internal record CreateMessageCommand:IRequest
{
    public string? ToUserId { get; set; }
    public string? Content { get; set; }
    public string? MediaId { get; set; }
}
