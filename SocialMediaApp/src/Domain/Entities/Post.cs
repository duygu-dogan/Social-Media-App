using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaApp.Domain.Entities;
public class Post: AuthorAuditableEntity
{
    public string? Content { get; set; }
    public string? Draft { get; set; }
    public Guid MediaId { get; set; }
    public IEnumerable<Media>? Medias { get; set; }
    public Post? AnswerTo { get; set; }
    public Guid? AnswerToId { get; set; }
    public IEnumerable<Post>? Answers { get; set; }
    public IEnumerable<Like>? Likes { get; set; }
    public IEnumerable<RePost>? RePosts { get; set; }

    public static Expression<Func<Post, bool>> LikedBySomeoneFollowedBy(Guid userId)
    {
        return p => (p.Likes ?? Enumerable.Empty<Like>())
                    .Any(l => l.CreatedBy != null &&
                              (l.CreatedBy.Followers ?? Enumerable.Empty<Follow>())
                    .Any(f => f.FollowerId == userId));
    }

    public static Expression<Func<Post, bool>> RePostedBySomeoneFollowedBy(Guid userId)
    {
        return p => (p.RePosts ?? Enumerable.Empty<RePost>())
                    .Any(r => r.CreatedBy != null && (r.CreatedBy.Followers ?? Enumerable.Empty<Follow>())
                    .Any(f => f.FollowerId == userId));
    }

    public static Expression<Func<Post, bool>> AuthorFollowedBy(Guid userId)
    {
        return p => (p.CreatedBy != null && (p.CreatedBy.Followers ?? Enumerable.Empty<Follow>()).Any(f => f.FollowedId == userId));
    }

    public static Expression<Func<Post, User?>> GetUserWhoLikedFollowedBy(Guid userId)
    {
        return p => (p.Likes ?? Enumerable.Empty<Like>())
                    .Where(l => l.CreatedBy != null &&
                            (l.CreatedBy.Followers ?? Enumerable.Empty<Follow>())
                                .Any(f => f.FollowerId == userId))
                    .Select(l => l.CreatedBy)
                    .FirstOrDefault();
     }

    public static Expression<Func<Post, User?>> GetUserWhoRePostedFollowedBy(Guid userId)
    {
        return p => (p.RePosts ?? Enumerable.Empty<RePost>())
                    .Where(r => r.CreatedBy != null && (r.CreatedBy.Followers ?? Enumerable.Empty<Follow>()).Any(f => f.FollowerId == userId))
                    .Select(r => r.CreatedBy)
                    .FirstOrDefault();
    }

    public static Expression<Func<Post, bool>> IsLikedBy(Guid userId)
    {
        return p => (p.Likes ?? Enumerable.Empty<Like>()).Any(l => l.CreatedById == userId);
    }

    public static Expression<Func<Post, bool>> IsRePostedBy(Guid userId)
    {
        return p => (p.RePosts ?? Enumerable.Empty<RePost>()).Any(r => r.CreatedById == userId);
    }
}
