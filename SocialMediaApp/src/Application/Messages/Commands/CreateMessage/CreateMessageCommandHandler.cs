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
        var fromUser = await _context.DomainUsers.FindAsync(_user.Id);

        if (toUser == null)
        {
            throw new NotFoundException(nameof(User), request.ToUserId!);
        }
        else if(fromUser == null)
        {
            throw new NotFoundException(nameof(User), _user.Id!);
        }

        var message = new Message
        {
            CreatedBy = fromUser,
            ToUserId = toUser.Id,
            Content = request.Content,
            MediaId = Guid.Parse(request.MediaId!)
        };

        message.AddDomainEvent(new MessageCreatedEvent(message));

        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
