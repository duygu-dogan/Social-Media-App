﻿using SocialMediaApp.Application.Common.Interfaces;
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
        //return await _context.Conversations.
        //    Where(c => c.Members != null && c.Members.Any(u => u.Id.ToString() == request.UserId || u.ApplicationUserId.ToString() == request.UserId))
        //    .OrderByDescending(c => c.Created)
        //    .ProjectTo<ConversationVM>(_mapper.ConfigurationProvider)
        //    .PaginatedListAsync(request.PageNumber, request.PageSize);

        return await _context.Conversations
            .Where(c => c.Members != null && c.Members.Any(u => u.Id.ToString() == request.UserId || u.ApplicationUserId.ToString() == request.UserId))
            .OrderByDescending(c => c.Created)
            .Select(c => new ConversationVM
            {
                Members = _context.DomainUsers
                    .Where(u => c.Members!.Any(m => m.Id == u.Id))
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .ToList(),
                Messages = _context.Messages
                    .Where(m => m.FromUserId.ToString() == request.UserId || m.ToUserId.ToString() == request.UserId)
                    .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                    .ToList()
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
