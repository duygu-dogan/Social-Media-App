using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Common.Interfaces;

namespace SocialMediaApp.Application.Follows.Queries.GetFollowSuggestions;
public class GetFollowSuggestionsQueryHandler : IRequestHandler<GetFollowSuggestionsQuery, IEnumerable<SuggestionUserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IMapper _mapper;

    public GetFollowSuggestionsQueryHandler(IApplicationDbContext context, IUser user, IMapper mapper)
    {
        _context = context;
        _user = user;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SuggestionUserDto>> Handle(GetFollowSuggestionsQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.DomainUsers
            .FirstOrDefaultAsync(u => u.ApplicationUserId.ToString() == _user.Id, cancellationToken);

        if (user == null)
            throw new ForbiddenAccessException();

        var requestedCount = request.Count ?? 10;

        var notFollowed = _context.DomainUsers
            .Where(u => u.Id != user.Id && !u.Followers.Any(f => f.FollowerId == user.Id));
        var notFollowedCount = await notFollowed.CountAsync(cancellationToken);

        var randomSkip = notFollowedCount > requestedCount ? new Random().Next(notFollowedCount - requestedCount) : 0;

        return await notFollowed.Skip(randomSkip)
            .Take(requestedCount)
            .ProjectTo<SuggestionUserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);            
    }
}
