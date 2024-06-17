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
    public abstract class NamedEntityConfiguration<T> : EntityConfiguration<T> where T : NamedEntity
    {

        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(x => x.Name);
        }
        //public void Configure(EntityTypeBuilder<T> builder)
        //{

        //    builder.HasIndex(x => x.Name);
        //    builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
        //    OnConfiguring(builder);
        //}

        //public abstract void OnConfiguring(EntityTypeBuilder<T> builder);
    }
}
