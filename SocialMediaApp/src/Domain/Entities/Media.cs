using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Domain.Entities;
public class Media: AuthorAuditableEntity
{
    public Post? Post { get; set; }
    public Guid PostId { get; set; }
    public string? Path { get; set; }
    public byte[]? Content { get; set; }
    public string? ContentType { get; set; }
    public string? FileName { get; set; }
}
