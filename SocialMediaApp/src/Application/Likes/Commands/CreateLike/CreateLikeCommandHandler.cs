using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Exceptions;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;

namespace SocialMediaApp.Application.Likes.Commands.CreateLike;
public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateLikeCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Unit> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {        
        var post = await _context.Posts
            .FirstOrDefaultAsync(p => p.Id.ToString() == request.PostId, cancellationToken);
        if (post == null)
            throw new NotFoundException(nameof(Post), request.PostId!);

        var liker = await _context.DomainUsers
            .FirstOrDefaultAsync(u => u.ApplicationUserId.ToString() == _user.Id, cancellationToken);
        if (liker == null)
            throw new ForbiddenAccessException();

        var alreadyLiked = await _context.Likes.AnyAsync(l => l.PostId == post.Id && l.CreatedBy!.Id == liker.Id, cancellationToken);
        if (!alreadyLiked)
        {
            var entity = new Like
            {
                PostId = post.Id,
                CreatedBy = liker
            };

            entity.AddDomainEvent(new LikeCreatedEvent(entity));

            await _context.Likes.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
