using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oceanic.Core;

namespace Oceanic.Infrastructure.Mapping
{
    public class CityMap : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Primary Key
            builder.HasKey(t => new { t.Id });

            // Table & Column Mappings
            builder.ToTable("CITY");
            builder.Property(t => t.Id).HasColumnName("ID");
            builder.Property(t => t.Code).HasColumnName("CODE");
            builder.Property(t => t.Name).HasColumnName("NAME");
            builder.Property(t => t.IsAcitve).HasColumnName("ISACTIVE");
        }
    }
}
