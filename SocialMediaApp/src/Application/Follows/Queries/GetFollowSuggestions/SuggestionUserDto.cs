using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Follows.Queries.GetFollowSuggestions;
public class SuggestionUserDto
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? ProfilePicture { get; set; }
}

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<User, SuggestionUserDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
            .ForMember(d => d.ProfilePicture, opt => opt.MapFrom(s => s.PictureId));
    }
}
