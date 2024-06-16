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
    public class PlaylistConfiguration : NamedEntityConfiguration<Playlist>
    {
        public override void OnConfiguring(EntityTypeBuilder<Playlist> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.Playlists).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.UserId });
        }
    }
}
