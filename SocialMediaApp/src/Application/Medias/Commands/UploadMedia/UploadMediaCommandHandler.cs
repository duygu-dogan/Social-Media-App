using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Medias.Commands.UploadMedia;
internal class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommand>
{
    private readonly IAzureStorageService _storage;
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public UploadMediaCommandHandler(IAzureStorageService storage, IApplicationDbContext context, IUser userService)
    {
        _storage = storage;
        _context = context;
        _user = userService;
    }

    public async Task Handle(UploadMediaCommand request, CancellationToken cancellationToken)
    {
       if(request == null)
            throw new ArgumentNullException(nameof(request));

       var path = $"posts/{request.PostId}";
       var files = request.Files;
       if (files == null)
            throw new ArgumentNullException(nameof(request.Files));
       
        var result = await _storage.UploadAsync(path, files);

        await _context.Medias.AddRangeAsync(result.Select(x => new Media
        {
            FileName = x.fileName,
            Path = x.path,
            Post = _context.Posts.Find(request.PostId),
            CreatedBy = _context.DomainUsers.Find(_user.Id)
        }).ToList());
        await _context.SaveChangesAsync(cancellationToken);
    }
}
