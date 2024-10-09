using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Common.Mappings;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Likes.Queries.GetLikesWithPagination;
internal class GetLikesWithPaginationQueryHandler : IRequestHandler<GetLikesWithPaginationQuery, PaginatedList<LikeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetLikesWithPaginationQueryHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
    }

    public async Task<PaginatedList<LikeDto>> Handle(GetLikesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Likes
            .Where(f => f.Id.ToString() == request.LikeId)
            .Select(Likes => new LikeDto
            {
                Id = Likes.Id.ToString(),
                LikerId = Likes.Liker!.Id.ToString(),
                Created = Likes.Created
            })
            .OrderByDescending(f => f.Created)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
