using System;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Conversations.Commands.CreateConversation;
public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateConversationCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<string> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var memberGuids = request.Members!.Select(id => Guid.Parse(id)).ToList();

        var members = await _context.DomainUsers
            .Where(u => memberGuids.Contains(u.Id) || u.ApplicationUserId.ToString() == _user.Id)
            .ToListAsync(cancellationToken);

        var notFounds = request.Members!.Where(id => !members.Any(found => found.Id.ToString() == id)).ToList();

        if (notFounds.Any())
            throw new NotFoundException(nameof(User), string.Join(", ", notFounds));

        var conversation = new Conversation
        {
            Members = members
        };

        _context.Conversations.Add(conversation);
        await _context.SaveChangesAsync(cancellationToken);

        return conversation.Id.ToString();
    }
}
