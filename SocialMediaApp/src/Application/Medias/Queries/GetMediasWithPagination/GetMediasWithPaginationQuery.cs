using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Medias.Queries.GetMediasWithPagination;
internal record GetMediasWithPaginationQuery: IRequest<PaginatedList<MediaDto>>
{
    public string? MediaId { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

}
