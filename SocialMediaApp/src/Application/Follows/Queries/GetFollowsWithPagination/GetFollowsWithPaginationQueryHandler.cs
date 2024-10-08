using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Application.Common.Mappings;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Follows.Queries.GetFollowsWithPagination;
public class GetFollowsWithPaginationQueryHandler : IRequestHandler<GetFollowsWithPaginationQuery, PaginatedList<FollowDto>>
{
    private readonly IApplicationDbContext _context;

    public GetFollowsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<FollowDto>> Handle(GetFollowsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Follows
            .Where(f => f.FollowerId.ToString() == request.FollowerId)
            .Select(f => new FollowDto
            {
                Id = f.Id.ToString(),
                FollowerId = f.FollowerId.ToString(),
                FollowedId = f.FollowedId.ToString(),
                Created = f.Created
            })
            .OrderByDescending(f => f.Created)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
