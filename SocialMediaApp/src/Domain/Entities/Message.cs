using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Message:AuthorAuditableEntity
{
    public string? Content { get; set; }
    public Media? Media { get; set; }
    public Guid MediaId { get; set; }
}
