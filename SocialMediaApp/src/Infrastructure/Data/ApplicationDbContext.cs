using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Application.Common.Interfaces;
using SocialMediaApp.Domain.Entities;
using SocialMediaApp.Infrastructure.Identity;

namespace SocialMediaApp.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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
}
