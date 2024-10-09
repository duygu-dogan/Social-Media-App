using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Notification: AuthorAuditableEntity
{ 
    public User? ForUser { get; set; }
    public Guid? ForUserId { get; set; }
    public Post? Post { get; set; }
    public Guid? PostId { get; set; }
    public bool IsRead { get; set; }
    public NotificationType Type { get; set; }
}
