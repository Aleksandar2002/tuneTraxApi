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
    internal class AlbumConfiguration : NamedEntityConfiguration<Album>
    {
        public override void OnConfiguring(EntityTypeBuilder<Album> builder)
        {
            builder.HasOne(x => x.Artist).WithMany(x => x.Albums).HasForeignKey(x => x.ArtistId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ArtistId);
        }
    }
}
