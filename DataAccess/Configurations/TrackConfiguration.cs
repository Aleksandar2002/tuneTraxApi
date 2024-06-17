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
    public class TrackConfiguration : EntityConfiguration<Track>
    {
        public override void OnConfiguring(EntityTypeBuilder<Track> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.AudioFile).IsRequired().HasMaxLength(300);

            builder.HasOne(x => x.Album).WithMany(x => x.Tracks).HasForeignKey(x => x.AlbumId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Playlists).WithMany(x => x.Tracks).UsingEntity<PlaylistTrack>();
            builder.HasMany(x => x.Artists).WithMany(x => x.Tracks).UsingEntity<ArtistTrack>();
            builder.HasMany(x => x.Genres).WithMany(x => x.Tracks).UsingEntity<GenreTrack>();

            builder.HasIndex(x => new { x.Title, x.ReleaseDate, x.AlbumId });
        }
    }
}
