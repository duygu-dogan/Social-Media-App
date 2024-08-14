using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Media: BaseAuditableEntity
{
    public byte[]? Content { get; set; }
    public string? ContentType { get; set; }
    public string? FileName { get; set; }
}
