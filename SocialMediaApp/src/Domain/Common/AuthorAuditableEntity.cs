using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Common;
public class AuthorAuditableEntity:BaseAuditableEntity
{
    public User? CreatedBy { get; set; }
    //public string? CreatedById { get; set; }
    public string? LastModifiedById { get; set; }
}
