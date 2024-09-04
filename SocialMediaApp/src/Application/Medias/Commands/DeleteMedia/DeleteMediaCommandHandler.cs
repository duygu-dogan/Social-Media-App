using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Medias.Commands.DeleteMedia;
internal class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand>
{
    private readonly IAzureStorageService _azureStorageService;
    private readonly IApplicationDbContext _context;

    public DeleteMediaCommandHandler(IAzureStorageService azureStorageService, IApplicationDbContext context)
    {
        _azureStorageService = azureStorageService;
        _context = context;
    }

    public async Task Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Medias.Where(x => x.Path == request.Path && x.FileName == request.FileName).FirstOrDefaultAsync();

        if (entity == null)
            throw new NotFoundException(nameof(Media), request.FileName!);

        if (request.Path == null || request.FileName == null)
            throw new ArgumentNullException(nameof(request.Path));

        await _azureStorageService.DeleteAsync(request.Path, request.FileName);
        _context.Medias.Remove(entity);

    }
}
