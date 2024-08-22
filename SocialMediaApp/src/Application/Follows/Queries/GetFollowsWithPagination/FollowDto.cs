using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Application.Follows.Queries.GetFollowsWithPagination;
public class FollowDto
{
    public string? Id { get; set; }
    public string? FollowerId { get; set; }
    public string? FollowedId { get; set; }
    public DateTime Created { get; set; }

}
