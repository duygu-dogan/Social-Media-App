using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Common.Mappings;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Medias.Queries.GetMediasWithPagination;
internal class GetMediasWithPaginationQueryHandler : IRequestHandler<GetMediasWithPaginationQuery, PaginatedList<MediaDto>>
{
    private readonly IApplicationDbContext _context;

    public GetMediasWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<MediaDto>> Handle(GetMediasWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Medias
            .Where(m=> m.Id.ToString() == request.MediaId)
            .Select(media => new MediaDto
        {
            MediaId = media.Id.ToString(),
            Path = media.Path,
            FileName = media.FileName,
            Created = media.Created
        })
            .OrderByDescending(m => m.Created)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
