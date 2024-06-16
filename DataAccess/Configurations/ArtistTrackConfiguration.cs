using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ArtistTrackConfiguration : IEntityTypeConfiguration<ArtistTrack>
{

    public void Configure(EntityTypeBuilder<ArtistTrack> builder)
    {
        builder.HasKey(x => new { x.ArtistId, x.TrackId });
    }


}
