using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Notifications.Queries.GetNotifications;
internal class NotificationDto
{
    public string? Id { get; set; }
    public bool IsRead { get; set; }
    public string? PostId { get; set; }
    public string? PostContent { get; set; }
    public UserDto? CreatedBy { get; set; }
    public NotificationType Type { get; set; }

    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Notification, NotificationDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(n => n.Id.ToString()))
                .ForMember(d => d.PostId, opt => opt.MapFrom(n => n.Post!.Id.ToString()))
                .ForMember(d => d.PostContent, opt => opt.MapFrom(n => n.Post!.Content))
                .ForMember(d => d.Type, opt => opt.MapFrom(n => n.Type));
        }
    }
}
