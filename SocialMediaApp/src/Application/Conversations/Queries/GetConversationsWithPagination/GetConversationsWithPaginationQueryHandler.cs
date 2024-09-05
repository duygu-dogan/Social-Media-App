using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Common.Mappings;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public class GetConversationsWithPaginationQueryHandler : IRequestHandler<GetConversationsWithPaginationQuery, PaginatedList<ConversationBriefDto>>
{
    private readonly IApplicationDbContext _context;

    public GetConversationsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ConversationBriefDto>> Handle(GetConversationsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Conversations.
            Where(c => c.Members != null && c.Members.Any(u => u.Id.ToString() == request.UserId || u.ApplicationUserId.ToString() == request.UserId))
            .Select(c => new ConversationBriefDto
                {
                    Id = c.Id.ToString(),
                    Content = c.Messages!.OrderByDescending(m => m.Created).Select(m => m.Content).FirstOrDefault(),
                    SenderId = c.Messages!.Select(m => m.CreatedBy!.Id).FirstOrDefault().ToString(),
                    ReciepentId = c.Members!.FirstOrDefault(u => u.Id.ToString() == request.UserId)!.Id.ToString(),
                    Created = c.Messages!.OrderByDescending(m => m.Created).Select(m => m.Created).FirstOrDefault()
                })
            .OrderBy(c => c.Created)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
