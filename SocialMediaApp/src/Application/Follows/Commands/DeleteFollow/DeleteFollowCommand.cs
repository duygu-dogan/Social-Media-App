using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Follows.Commands.DeleteFollow;
public record DeleteFollowCommand(): IRequest
{
    public string? FollowedId { get; set; }
    public string? FollowerId { get; set; }

}
