using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oceanic.Core;

namespace Oceanic.Infrastructure.Mapping
{
    public class PriceMap : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            // Primary Key
            builder.HasKey(t => new { t.Id });

            // Table & Column Mappings
            builder.ToTable("PRICE");
            builder.Property(t => t.SizeId).HasColumnName("SIZEID");
            builder.Property(t => t.MaxWeight).HasColumnName("MAXWEIGHT");
            builder.Property(t => t.Fee).HasColumnName("PRICE");
        }
    }
}
