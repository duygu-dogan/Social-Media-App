using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Domain.Events;

namespace SocialMediaApp.Application.Messages.Commands.CreateMessage;
internal class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateMessageCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var toUser = await _context.DomainUsers.FindAsync(Guid.Parse(request.ToUserId!));
        var fromUser = await _context.DomainUsers.Where(u => u.ApplicationUserId.ToString() == _user.Id).FirstOrDefaultAsync();
        var conversation = await _context.Conversations.FindAsync(Guid.Parse(request.ConversationId!));

        if (toUser == null)
        {
            throw new NotFoundException(nameof(User), request.ToUserId!);
        }
        else if(fromUser == null)
        {
            throw new NotFoundException(nameof(User), _user.Id!);
        }
        else if(conversation == null)
        {
            throw new NotFoundException(nameof(conversation), request.ConversationId!);
        }

        var message = new Message
        {
            CreatedBy = fromUser,
            ToUserId = toUser.Id,
            Content = request.Content,
            IsRead = request.IsRead,
            ConversationId = request.ConversationId == null ? Guid.Empty : Guid.Parse(request.ConversationId),
            Media = request.MediaId == null ? null : new Media { Id = Guid.Parse(request.MediaId!) }
        };

        message.AddDomainEvent(new MessageCreatedEvent(message));

        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
