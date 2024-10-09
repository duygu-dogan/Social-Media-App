using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Like: BaseAuditableEntity
{
    public Post? Post { get; set; }
    public Guid PostId { get; set; }
    public User? Liker { get; set; }
    public Guid LikerId { get; set; }
    public bool IsLiked { get; set; } = true;
}
