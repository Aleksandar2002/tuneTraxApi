using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
{
    // public override void OnConfiguring(EntityTypeBuilder<PlaylistTrack> builder)
    // {
    //     builder.HasKey(x => new { x.PlaylistId, x.TrackId });
    // }
    public void Configure(EntityTypeBuilder<PlaylistTrack> builder)
    {
        builder.HasKey(x => new { x.PlaylistId, x.TrackId });
    }
}
