using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class User: BaseAuditableEntity
{
    public Guid ApplicationUserId { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    //public Media? Picture { get; set; }
    //public  Guid PictureId { get; set; }
    //public Media? Banner { get; set; }
    //public Guid BannerId { get; set; }
    public string? Location { get; set; }
    public string? Bio { get; set; }
    public string? WebSite { get; set; }
    
    public IEnumerable<Follow> Followers { get; set; } = new List<Follow>();
    public IEnumerable<Follow> Followeds { get; set; } = new List<Follow>();
    public IEnumerable<Conversation> Conversations { get; set; } = new List<Conversation>();
}
