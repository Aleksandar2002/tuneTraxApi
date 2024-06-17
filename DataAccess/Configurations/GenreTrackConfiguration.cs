using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class GenreTrackConfiguration : IEntityTypeConfiguration<GenreTrack>
{
    public void Configure(EntityTypeBuilder<GenreTrack> builder)
    {
        builder.HasKey(x => new { x.GenreId, x.TrackId });
    }

}
