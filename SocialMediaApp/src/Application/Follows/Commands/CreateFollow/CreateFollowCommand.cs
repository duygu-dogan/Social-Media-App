using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Follows.Commands.CreateFollow;
public record CreateFollowCommand: IRequest<Unit>
{
    public string? FollowedId { get; init; }
}
