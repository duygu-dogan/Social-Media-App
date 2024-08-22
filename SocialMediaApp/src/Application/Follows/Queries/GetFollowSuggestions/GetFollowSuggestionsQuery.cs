
namespace SocialMediaApp.Application.Follows.Queries.GetFollowSuggestions;
public record GetFollowSuggestionsQuery: IRequest<IEnumerable<SuggestionUserDto>>
{
    public int? Count { get; init; }
}
