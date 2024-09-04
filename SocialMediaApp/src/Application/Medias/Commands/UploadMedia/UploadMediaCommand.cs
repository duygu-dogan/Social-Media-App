using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SocialMediaApp.Application.Medias.Commands.UploadMedia;
internal record UploadMediaCommand:IRequest
{
    public string? PostId { get; set; }
    public string? UserId { get; set; }
    public string? FileName { get; set; }
    public IFormFileCollection? Files { get; set; }
}
