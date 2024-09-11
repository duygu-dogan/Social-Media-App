using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Medias.Queries.GetMediasWithPagination;
internal class GetMediasWithPaginationQueryValidator : AbstractValidator<GetMediasWithPaginationQuery>
{
    public GetMediasWithPaginationQueryValidator()
    {
        RuleFor(x => x.MediaId)
            .NotEmpty();
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
