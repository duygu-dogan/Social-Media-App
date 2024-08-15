using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Infrastructure.Data.Configurations;
public class LikeConfiguration: IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(l => new { l.CreatedById, l.PostId });
    }
}
