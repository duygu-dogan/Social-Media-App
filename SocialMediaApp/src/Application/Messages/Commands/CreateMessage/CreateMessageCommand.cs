﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Messages.Commands.CreateMessage;
public record CreateMessageCommand:IRequest
{
    public string? ToUserId { get; set; }
    public string? Content { get; set; }
    public string? ConversationId { get; set; }
    public string? MediaId { get; set; } = null;
    public bool IsRead { get; set; }
}
