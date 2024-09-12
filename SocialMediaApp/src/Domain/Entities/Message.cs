using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Message:AuthorAuditableEntity
{
    public Guid FromUserId { get; set; }
    public User? FromUser { get; set; }
    public Guid ToUserId { get; set; }
    public User? ToUser { get; set; }
    public string? Content { get; set; }
    public Media? Media { get; set; }
    public Guid MediaId { get; set; }
}
