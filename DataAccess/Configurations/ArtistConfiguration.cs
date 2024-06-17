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
    public class ArtistConfiguration : NamedEntityConfiguration<Artist>
    {
        public override void OnConfiguring(EntityTypeBuilder<Artist> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);

            builder.HasOne(x => x.Country).WithMany(x => x.Artists).HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Genres).WithMany(x => x.Artists).UsingEntity<ArtistGenre>();

            builder.HasIndex(x => new { x.FirstName, x.LastName, x.CountryId });
        }
    }
}
