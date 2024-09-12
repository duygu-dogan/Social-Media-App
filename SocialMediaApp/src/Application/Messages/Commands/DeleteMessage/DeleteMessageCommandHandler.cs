using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Messages.Commands.DeleteMessage;
internal class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _context.Messages.FindAsync(Guid.Parse(request.Id!));

        Guard.Against.NotFound(request.Id!, message);

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
