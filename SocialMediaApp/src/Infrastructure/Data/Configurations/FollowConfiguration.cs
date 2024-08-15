using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Infrastructure.Data.Configurations;
public class FollowConfiguration: IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.HasKey(f => new {f.FollowerId, f.FollowedId});
        builder.HasOne(f => f.Followed).WithMany(u => u.Followers);
        builder.HasOne(f => f.Follower).WithMany(u => u.Followeds);
    }
}
