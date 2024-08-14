using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class RePost: AuthorAuditableEntity
{
    public Post? Post { get; set; }
    public Guid PostId { get; set; }
}
