using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public record GetConversationsWithPaginationQuery: IRequest<PaginatedList<ConversationBriefDto>>
{
    public string? UserId { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
