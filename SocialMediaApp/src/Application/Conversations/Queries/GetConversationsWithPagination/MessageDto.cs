using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public class MessageDto
{
    public string? FromUserId { get; set; }
    public string? ToUserId { get; set; }
    public string? Content { get; set; }

    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(d => d.FromUserId, opt => opt.MapFrom(s => s.FromUserId.ToString()))
                .ForMember(d => d.ToUserId, opt => opt.MapFrom(s => s.ToUserId.ToString()));
        }
    }
}
