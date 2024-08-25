using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Likes.Commands.CreateLike;
public record CreateLikeCommand: IRequest<Unit>
{
    public string? PostId { get; set; }
}
