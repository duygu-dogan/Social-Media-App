using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Infrastructure.Data.Configurations;
public class RePostConfiguration: IEntityTypeConfiguration<RePost>
{
    public void Configure(EntityTypeBuilder<RePost> builder)
    {
        builder.HasKey(r => new { r.UserId, r.PostId });
    }
}

