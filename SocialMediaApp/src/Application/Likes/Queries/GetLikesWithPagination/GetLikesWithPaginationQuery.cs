using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Likes.Queries.GetLikesWithPagination;
public record GetLikesWithPaginationQuery: IRequest<PaginatedList<LikeDto>>
{
    public string? LikerId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
