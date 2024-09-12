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
    private readonly IMapper _mapper;

    public GetMessageWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MessageDto>> Handle(GetMessageWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Messages
            .Where(m => m.Id.ToString() == request.MessageId)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(m => m.Created)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
