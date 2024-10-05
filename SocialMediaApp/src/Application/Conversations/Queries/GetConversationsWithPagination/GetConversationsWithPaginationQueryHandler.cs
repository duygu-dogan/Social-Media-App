using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Common.Mappings;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public class GetConversationsWithPaginationQueryHandler : IRequestHandler<GetConversationsWithPaginationQuery, PaginatedList<ConversationVM>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetConversationsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ConversationVM>> Handle(GetConversationsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var conversations = await _context.Conversations
        .Where(c => c.Members!.Any(u => u.Id.ToString() == request.UserId || u.ApplicationUserId.ToString() == request.UserId))
        .Include(c => c.Members)
        .Include(c => c.Messages)
        .OrderByDescending(c => c.Created)
        .ToListAsync();

        var conversationVMs = conversations.Select(c => new ConversationVM
        {
            Members = c.Members!.Select(m => _mapper.Map<UserDto>(_context.DomainUsers.FirstOrDefault(u => u.Id == m.Id))).ToList(),
            LastMessage = c.Messages!.Select(m => _mapper.Map<MessageDto>(_context.Messages.FirstOrDefault(ms => ms.ConversationId == c.Id))).LastOrDefault()
        }).ToList();

        return new PaginatedList<ConversationVM>(conversationVMs, conversationVMs.Count, request.PageNumber, request.PageSize);
    }
}
