using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Conversation> Conversations { get; }
    DbSet<Message> Messages { get; }
    DbSet<Follow> Follows { get; }
    DbSet<Like> Likes { get; }
    DbSet<Post> Posts { get; }
    DbSet<User> DomainUsers { get; }
    DbSet<Media> Medias { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<RePost> RePosts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
