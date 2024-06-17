using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class GenreConfiguration :  NamedEntityConfiguration<Genre>
    {
        public override void OnConfiguring(EntityTypeBuilder<Genre> builder)
        {

        }
    }
}
