using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public class UserDto
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }

    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}

