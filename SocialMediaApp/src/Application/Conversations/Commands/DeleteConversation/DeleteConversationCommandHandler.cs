using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;

namespace SocialMediaApp.Application.Conversations.Commands.DeleteConversation;
public class DeleteConversationCommandHandler : IRequestHandler<DeleteConversationCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteConversationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _context.Conversations.FindAsync(Guid.Parse(request.id), cancellationToken);

        Guard.Against.NotFound(request.id, conversation);
        _context.Conversations.Remove(conversation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
