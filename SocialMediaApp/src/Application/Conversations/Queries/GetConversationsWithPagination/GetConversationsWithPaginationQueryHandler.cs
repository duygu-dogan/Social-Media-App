using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Common.Mappings;
using SocialMediaApp.Application.Common.Models;
using SocialMediaApp.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public class GetConversationsWithPaginationQueryHandler : IRequestHandler<GetConversationsWithPaginationQuery, PaginatedList<ConversationBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetConversationsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ConversationBriefDto>> Handle(GetConversationsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Conversations.
            Where(c => c.Members != null && c.Members.Any(u => u.Id.ToString() == request.UserId || u.ApplicationUserId.ToString() == request.UserId))
            .Select(c => new ConversationBriefDto
                {
                    Id = c.Id.ToString(),
                    Content = c.Messages!.OrderByDescending(m => m.Created).Select(m => m.Content).FirstOrDefault(),
                    SenderId = c.Messages!.Select(m => m.CreatedById.ToString()).FirstOrDefault(),
                    ReciepentId = c.Members!.FirstOrDefault(u => u.Id.ToString() == request.UserId)!.Id.ToString(),
                    Created = c.Messages!.OrderByDescending(m => m.Created).Select(m => m.Created).FirstOrDefault()
                })
            .OrderBy(c => c.Created)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
