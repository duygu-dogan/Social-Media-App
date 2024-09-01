using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Messages.Queries.GetMessagesWithPagination;
public class MessageBriefDto
{
    public string? Id { get; set; }
    public string? Content { get; set; }
    public string? MediaId { get; set; }
    public string? SenderId { get; set; }
    //public string? RecipientId { get; set; }
    public DateTime Created { get; set; }

    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Message, MessageBriefDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
                .ForMember(d => d.MediaId, opt => opt.MapFrom(s => s.MediaId))
                .ForMember(d => d.SenderId, opt => opt.MapFrom(s => s.CreatedById))
                .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created));
        }
    }
}
