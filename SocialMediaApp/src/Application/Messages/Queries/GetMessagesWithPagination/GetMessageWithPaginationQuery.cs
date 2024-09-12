using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.Messages.Queries.GetMessagesWithPagination;
internal class GetMessageWithPaginationQuery: IRequest<PaginatedList<MessageDto>>
{
    public string? MessageId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
