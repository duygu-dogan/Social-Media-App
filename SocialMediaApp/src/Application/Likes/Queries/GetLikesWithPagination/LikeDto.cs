using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Likes.Queries.GetLikesWithPagination;
public class LikeDto
{
    public string? Id { get; set; }
    public string? LikerId { get; set; }
    public DateTime Created { get; set; }
}
