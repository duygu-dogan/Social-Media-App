using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;

namespace SocialMediaApp.Application.Follows.Commands.DeleteFollow;
public class DeleteFollowCommandHandler : IRequestHandler<DeleteFollowCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteFollowCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteFollowCommand request, CancellationToken cancellationToken)
    {
        var follow = await _context.Follows.FindAsync(new object[] { Guid.Parse(request.FollowerId!), Guid.Parse(request.FollowedId!) }, cancellationToken);

        Guard.Against.NotFound(request.FollowedId!, follow);

       _context.Follows.Remove(follow!);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
