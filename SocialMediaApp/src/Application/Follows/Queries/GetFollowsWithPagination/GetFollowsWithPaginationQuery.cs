using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Follows.Queries.GetFollowsWithPagination;
public record GetFollowsWithPaginationQuery: IRequest<PaginatedList<FollowDto>>
{
    public string? FollowId { get; init; }
    public int PageSize { get; init; } = 10;
    public int PageNumber { get; init; } = 1;
}
