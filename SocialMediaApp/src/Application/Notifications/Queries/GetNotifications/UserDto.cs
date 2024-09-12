using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Notifications.Queries.GetNotifications;
internal class UserDto
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? Username { get; set; }

    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(u => u.Id.ToString()));
        }
    }
}
