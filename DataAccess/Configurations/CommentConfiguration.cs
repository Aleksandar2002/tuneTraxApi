using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class CommentConfiguration : EntityConfiguration<Comment>
    {
        public override void OnConfiguring(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Track).WithMany(x => x.Comments).HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.TrackId, x.UserId, x.CreatedAt });
        }
    }
}
