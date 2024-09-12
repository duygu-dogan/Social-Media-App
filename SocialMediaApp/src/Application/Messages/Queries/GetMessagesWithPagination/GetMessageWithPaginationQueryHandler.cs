using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Common.Mappings;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Messages.Queries.GetMessagesWithPagination;
internal class GetMessageWithPaginationQueryHandler : IRequestHandler<GetMessageWithPaginationQuery, PaginatedList<MessageDto>>
{
    private readonly IApplicationDbContext _context;

    public GetMessageWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<MessageDto>> Handle(GetMessageWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Messages
            .Where(m => m.Id.ToString() == request.MessageId)
            .Select(message => new MessageDto
        {
            Content = message.Content,
            Created = message.Created,
            Id = message.Id.ToString(),
            MediaId = message.MediaId.ToString(),
            SenderId = message.CreatedBy!.Id.ToString(),
            ToUserId = message.ToUserId.ToString()
        })
            .OrderByDescending(m => m.Created)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
