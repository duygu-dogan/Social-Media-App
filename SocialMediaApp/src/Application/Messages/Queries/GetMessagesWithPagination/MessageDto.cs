using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Messages.Queries.GetMessagesWithPagination;
public class MessageDto
{
    public string? Id { get; set; }
    public string? Content { get; set; }
    public string? MediaId { get; set; }
    public string? SenderId { get; set; }
    public string? ToUserId { get; set; }
    public DateTime Created { get; set; }
}
