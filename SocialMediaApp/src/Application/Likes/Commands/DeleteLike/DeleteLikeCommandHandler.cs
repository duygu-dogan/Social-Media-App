﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Likes.Commands.DeleteLike;
public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLikeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {

        var like = await _context.Likes.FindAsync(new object[] { Guid.Parse(request.PostId!), Guid.Parse(request.LikerId!) }, cancellationToken);

        if(like == null)
        {
            throw new NotFoundException(nameof(Like), request.LikerId!);
        }

        _context.Likes.Remove(like);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
