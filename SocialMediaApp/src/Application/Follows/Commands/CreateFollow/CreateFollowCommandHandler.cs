using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;

namespace SocialMediaApp.Application.Follows.Commands.CreateFollow;
public class CreateFollowCommandHandler : IRequestHandler<CreateFollowCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateFollowCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Unit> Handle(CreateFollowCommand request, CancellationToken cancellationToken)
    {
        if(request.FollowedId == null)
            throw new BadRequestException("FollowedId is required");

        var follewer = await _context.DomainUsers
            .FirstOrDefaultAsync(u => u.ApplicationUserId.ToString() == _user.Id, cancellationToken);
        if(follewer == null)
                throw new ForbiddenAccessException();

        var followed = await _context.DomainUsers
            .FirstOrDefaultAsync(f => f.Id.ToString() == request.FollowedId, cancellationToken);
        if (followed == null)
            throw new NotFoundException(nameof(User), request.FollowedId!);

        var alreadyFollowed = await _context.Follows
            .AnyAsync(f => f.FollowerId == follewer.Id && f.FollowedId == followed.Id, cancellationToken);
        if (alreadyFollowed)
            return Unit.Value;

        var entity = new Follow
        {
            FollowerId = follewer.Id,
            FollowedId = followed.Id
        };

        entity.AddDomainEvent(new FollowCreatedEvent(entity));

        await _context.Follows.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
