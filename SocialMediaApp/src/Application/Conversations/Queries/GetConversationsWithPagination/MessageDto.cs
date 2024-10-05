using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Conversations.Queries.GetConversationsWithPagination;
public class MessageDto
{
    public string? CreatedById { get; set; }
    public string? ToUserId { get; set; }
    public string? Content { get; set; }
    public string? ConversationId { get; set; }
    public bool IsRead { get; set; }

    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(m => m.CreatedById, opt => opt.MapFrom(s => s.CreatedBy!.Id.ToString()))
                .ForMember(m => m.ToUserId, opt => opt.MapFrom(s => s.ToUserId.ToString()))
                .ForMember(m => m.ConversationId, opt => opt.MapFrom(s => s.ConversationId.ToString()));
        }
    }
}
