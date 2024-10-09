using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Likes.Commands.DeleteLike;
public record DeleteLikeCommand(): IRequest
{
    public string? PostId { get; set; }
    public string? LikerId { get; set; }
}
