using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Messages.Queries.GetMessagesWithPagination;
internal class GetMessageWithPaginationValidator: AbstractValidator<GetMessageWithPaginationQuery>
{
    public GetMessageWithPaginationValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than 0");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0");
    }
}
