using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oceanic.Core;

namespace Oceanic.Infrastructure.Mapping
{
    public class TransportTypeMap : IEntityTypeConfiguration<TransportType>
    {
        public void Configure(EntityTypeBuilder<TransportType> builder)
        {
            // Primary Key
            builder.HasKey(t => new { t.Id });

            // Table & Column Mappings
            builder.ToTable("TRANSPORTTYPE");
            builder.Property(t => t.Id).HasColumnName("ID");
            builder.Property(t => t.Code).HasColumnName("CODE");
            builder.Property(t => t.Name).HasColumnName("NAME");
        }
    }
}
