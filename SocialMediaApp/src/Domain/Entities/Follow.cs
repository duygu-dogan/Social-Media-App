using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Follow: BaseAuditableEntity
{
    public User? Follower { get; set; }
    public Guid FollowerId { get; set; }
    public User? Followed { get; set; }
    public Guid FollowedId { get; set; }
    public bool IsFollowed { get; set; } = true;
}
