﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Messages.Queries.GetMessagesWithPagination;
public class MessageDto
{
    public string? Id { get; set; }
    public string? Content { get; set; }
    public string? MediaId { get; set; }
    public string? FromUserId { get; set; }
    public string? ToUserId { get; set; }
    public DateTime Created { get; set; }

    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(d => d.MediaId, opt => opt.MapFrom(s => s.MediaId.ToString()))
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.FromUserId, opt => opt.MapFrom(s => s.FromUserId.ToString()))
                .ForMember(d => d.ToUserId, opt => opt.MapFrom(s => s.ToUserId.ToString()));
        }
    }
}
