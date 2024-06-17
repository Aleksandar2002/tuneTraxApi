using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class UseCaseLogConfiguration : IEntityTypeConfiguration<UseCaseLog>
{
    public void Configure(EntityTypeBuilder<UseCaseLog> builder)
    {
        builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UseCaseName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UseCaseData).HasMaxLength(1500);
        builder.Property(x => x.AttemptedAt).HasDefaultValueSql("GETDATE()");


        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Username);
        builder.HasIndex(x => x.UseCaseName);
        builder.HasIndex(x => x.AttemptedAt);
    }

}
