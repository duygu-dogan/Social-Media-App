using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;

namespace SocialMediaApp.Application.Conversations.Commands;
public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateConversationCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public Task<string> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var members = _context.DomainUsers.
                        Where(u => request.Members.Any(id => id == u.Id.ToString()) || u.ApplicationUserId.ToString() == _user.Id)
                        .ToListAsync(cancellationToken);
        var entity = new Conversation
        {
            Members = request.Members,
        };
        entity.AddDomainEvent(new ConversationCreatedEvent(entity));
        _context.Conversations.Add(entity);
        _context.SaveChangesAsync(cancellationToken);

        return Task.CompletedTask;
    }
}
