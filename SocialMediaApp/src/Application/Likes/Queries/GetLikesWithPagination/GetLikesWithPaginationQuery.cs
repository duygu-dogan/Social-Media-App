using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Likes.Queries.GetLikesWithPagination;
internal class GetLikesWithPaginationQuery: IRequest<PaginatedList<LikeDto>>
{
    public string? LikeId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
