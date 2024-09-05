using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Infrastructure.Identity;

namespace SocialMediaApp.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IUserNotificationService? _userNotificationService;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserNotificationService? userNotificationService = null) : base(options) 
    {
        _userNotificationService = userNotificationService;
    }

    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<Message> Messages =>Set<Message>();
    public DbSet<Follow> Follows => Set<Follow>();
    public DbSet<Like> Likes => Set<Like>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Media> Medias => Set<Media>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<RePost> RePosts => Set<RePost>();
    public DbSet<User> DomainUsers => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    private async Task SendNotifications(IEnumerable<Notification> notifications, CancellationToken cancellationToken)
    {
        foreach (var notification in notifications)
        {
            var notificationWithRelateds = await Notifications.Include(n => n.Post)
                 .Include(n => n.CreatedBy)
                 .Include(n => n.ForUser)
                 .FirstOrDefaultAsync(n => n.Id == notification.Id, cancellationToken);

            if (notificationWithRelateds != null)
                _userNotificationService?.SendNotification(notificationWithRelateds);
        }
    }
}
