using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class InvalidTokenConfiguration : IEntityTypeConfiguration<InvalidToken>
{
    public void Configure(EntityTypeBuilder<InvalidToken> builder)
    {
        builder.Property(x => x.AddedAt).HasDefaultValueSql("GETDATE()");
        // builder.Property(x => x.Token).IsRequired().HasMaxLength(700);
        // builder.HasIndex(x => x.Token).IsUnique();
        builder.HasIndex(x => x.TokenId).IsUnique();
        builder.HasIndex(x => new { x.UserId, x.AddedAt });
        builder.HasKey(x => x.TokenId);
    }

}
