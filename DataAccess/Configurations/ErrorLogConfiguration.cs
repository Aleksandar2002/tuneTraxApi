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
    public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.StackTrace).IsRequired();
            builder.Property(x => x.ActorUsername).IsRequired();

            builder.HasKey(x => x.ErrorId);

            builder.HasIndex(x => x.ErrorId);
            builder.HasIndex(x => x.ActorUsername);
        }
    }
}
