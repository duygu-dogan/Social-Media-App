using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Infrastructure.Data.Configurations;
public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.ApplicationUserId).IsUnique();
        builder.Property(u => u.FullName).HasMaxLength(60);
        builder.Property(u => u.UserName).HasMaxLength(30);
        builder.Property(u => u.Bio).HasMaxLength(140);
        //builder.Property(u => u.Picture).HasMaxLength(255);
        builder.Property(u => u.WebSite).HasMaxLength(100);
        builder.Property(u => u.Location).HasMaxLength(30);
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasMany(u => u.Conversations).WithMany(c => c.Members);
    }
}
